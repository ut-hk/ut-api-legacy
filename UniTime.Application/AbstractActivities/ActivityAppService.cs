using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper.QueryableExtensions;
using UniTime.AbstractActivities.Dtos;
using UniTime.Activities;
using UniTime.Activities.Managers;
using UniTime.Descriptions;
using UniTime.Descriptions.Dtos;
using UniTime.Locations.Managers;
using UniTime.Ratings;
using UniTime.Tags;

namespace UniTime.AbstractActivities
{
    public class ActivityAppService : UniTimeAppServiceBase, IActivityAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IActivityManager _activityManager;
        private readonly IRepository<Description, long> _descriptionRepository;
        private readonly ILocationManager _locationManager;
        private readonly IRepository<Rating, long> _ratingRepository;
        private readonly IRepository<Tag, long> _tagRepository;

        public ActivityAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IRepository<Rating, long>ratingRepository,
            IRepository<Description, long> descriptionRepository,
            IRepository<Tag, long> tagRepository,
            IActivityManager activityManager,
            ILocationManager locationManager)
        {
            _abstractActivityRepository = abstractActivityRepository;
            _ratingRepository = ratingRepository;
            _descriptionRepository = descriptionRepository;
            _tagRepository = tagRepository;
            _activityManager = activityManager;
            _locationManager = locationManager;
        }

        public async Task<GetActivityOutput> GetActivity(EntityDto<Guid> input)
        {
            var activity = await _abstractActivityRepository.GetAll()
                .OfType<Activity>()
                .Include(a => a.Descriptions)
                .Include(a => a.Location)
                .Include(a => a.Tags)
                .Include(a => a.Ratings)
                .Include(a => a.Comments)
                .Include(a => a.Owner)
                .Include(a => a.Participants)
                .FirstOrDefaultAsync(a => a.Id == input.Id);

            if (activity == null)
                throw new UserFriendlyException(_activityManager.DoesNotExistMessage);

            return new GetActivityOutput
            {
                Activity = activity.MapTo<ActivityDto>()
            };
        }

        public async Task<GetActivitiesOutput> GetActivities(GetActivitiesInput input)
        {
            var activityListDtos = await _abstractActivityRepository.GetAll()
                .OfType<Activity>()
                .Include(activity => activity.Location)
                .Include(activity => activity.Tags)
                .Include(activity => activity.Owner)
                .Where(activity =>
                    activity.OwnerId == input.UserId
                )
                .OrderBy(activity => activity.StartTime)
                .ProjectTo<ActivityListDto>()
                .ToListAsync();

            await InjectCoverDescriptionAsync(activityListDtos);
            await InjectMyRatingStatusAsync(activityListDtos);

            return new GetActivitiesOutput
            {
                Activities = activityListDtos
            };
        }

        [AbpAuthorize]
        public async Task<GetMyActivitiesOutput> GetMyActivities()
        {
            var currentUserId = GetCurrentUserId();

            var myActivityListDtos = await _abstractActivityRepository.GetAll()
                .OfType<Activity>()
                .Include(activity => activity.Location)
                .Include(activity => activity.Tags)
                .Include(activity => activity.Owner)
                .Where(activity =>
                    activity.OwnerId == currentUserId ||
                    activity.Participants.Select(participant => participant.OwnerId).Contains(currentUserId)
                )
                .OrderBy(activity => activity.StartTime)
                .ProjectTo<ActivityListDto>()
                .ToListAsync();

            await InjectCoverDescriptionAsync(myActivityListDtos);
            await InjectMyRatingStatusAsync(myActivityListDtos);

            return new GetMyActivitiesOutput
            {
                MyActivities = myActivityListDtos.MapTo<List<ActivityListDto>>()
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivity(CreateActivityInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var location = input.LocationId.HasValue ? await _locationManager.GetLocationAsync(input.LocationId.Value) : null;

            var activity = await _activityManager.CreateAsync(Activity.Create(
                input.Name,
                input.StartTime,
                input.EndTime,
                location,
                currentUser
            ));

            return new EntityDto<Guid>(activity.Id);
        }

        [AbpAuthorize]
        public async Task UpdateActivity(UpdateActivityInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activity = await _activityManager.GetAsync(input.Id);
            var location = input.LocationId.HasValue ? await _locationManager.GetLocationAsync(input.LocationId.Value) : null;
            var tags = await _tagRepository.GetAllListAsync(tag => input.TagIds.Contains(tag.Id));

            _activityManager.EditActivity(activity, input.Name, input.StartTime, input.EndTime, location, tags, currentUserId);
            _activityManager.EditDescriptions(activity, input.DescriptionIds, currentUserId);
        }

        private async Task InjectMyRatingStatusAsync(ICollection<ActivityListDto> activityListDtos)
        {
            var currentUserId = AbpSession.UserId;

            if (currentUserId.HasValue)
            {
                var acitivtyIds = activityListDtos.Select(activity => activity.Id);

                var activityRatingStatusDictionary = await _ratingRepository.GetAll()
                    .Where(rating => rating.OwnerId == currentUserId.Value && rating.AbstractActivityId != null && acitivtyIds.Contains(rating.AbstractActivityId.Value))
                    .GroupBy(rating => rating.AbstractActivityId.Value)
                    .ToDictionaryAsync(rating => rating.Key, ratings => ratings.FirstOrDefault());

                foreach (var activityListDto in activityListDtos)
                {
                    var activityId = activityListDto.Id;

                    if (activityRatingStatusDictionary.ContainsKey(activityId))
                        activityListDto.MyRatingStatus = activityRatingStatusDictionary[activityId]?.RatingStatus;
                }
            }
        }

        private async Task InjectCoverDescriptionAsync(ICollection<ActivityListDto> activityListDtos)
        {
            var acitivtyIds = activityListDtos.Select(activity => activity.Id);

            var activityImageDescriptionDictionary = await _descriptionRepository.GetAll()
                .Where(d => d is ExternalImageDescription || d is InternalImageDescription)
                .Where(description => description.AbstractActivityId != null && acitivtyIds.Contains(description.AbstractActivityId.Value))
                .GroupBy(description => description.AbstractActivityId.Value)
                .ToDictionaryAsync(a => a.Key, a => a.FirstOrDefault());

            var activityTextDescriptionDictionary = await _descriptionRepository.GetAll()
                .Where(d => d is TextDescription)
                .Where(description => description.AbstractActivityId != null && acitivtyIds.Contains(description.AbstractActivityId.Value))
                .GroupBy(description => description.AbstractActivityId.Value)
                .ToDictionaryAsync(a => a.Key, a => a.FirstOrDefault());

            foreach (var activityListDto in activityListDtos)
            {
                var activityId = activityListDto.Id;

                if (activityImageDescriptionDictionary.ContainsKey(activityId))
                    activityListDto.CoverImageDescription = activityImageDescriptionDictionary[activityId].MapTo<DescriptionDto>();

                if (activityTextDescriptionDictionary.ContainsKey(activityId))
                    activityListDto.CoverTextDescription = activityTextDescriptionDictionary[activityId].MapTo<DescriptionDto>();
            }
        }
    }
}