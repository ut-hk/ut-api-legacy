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
using UniTime.Descriptions.Enums;
using UniTime.Descriptions.Managers;
using UniTime.Locations.Managers;
using UniTime.Ratings;
using UniTime.Ratings.Enums;
using UniTime.Tags.Managers;

namespace UniTime.AbstractActivities
{
    public class ActivityAppService : UniTimeAppServiceBase, IActivityAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IActivityManager _activityManager;
        private readonly IActivityTemplateManager _activityTemplateManager;
        private readonly IDescriptionManager _descriptionManager;
        private readonly IRepository<Description, long> _descriptionRepository;
        private readonly ILocationManager _locationManager;
        private readonly IRepository<Rating, long> _ratingRepository;
        private readonly ITagManager _tagManager;

        public ActivityAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IRepository<Rating, long>ratingRepository,
            IRepository<Description, long> descriptionRepository,
            IDescriptionManager descriptionManager,
            ITagManager tagManager,
            IActivityManager activityManager,
            IActivityTemplateManager activityTemplateManager,
            ILocationManager locationManager)
        {
            _abstractActivityRepository = abstractActivityRepository;
            _ratingRepository = ratingRepository;
            _descriptionRepository = descriptionRepository;
            _descriptionManager = descriptionManager;
            _tagManager = tagManager;
            _activityManager = activityManager;
            _activityTemplateManager = activityTemplateManager;
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
            await InjectLikesAsync(activityListDtos);
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
            await InjectLikesAsync(myActivityListDtos);
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
            var tags = await _tagManager.GetTags(input.TagTexts);

            var activity = await _activityManager.CreateAsync(Activity.Create(
                input.Name,
                input.StartTime,
                input.EndTime,
                location,
                tags,
                currentUser
            ));

            return new EntityDto<Guid>(activity.Id);
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivityFromActivityTemplate(CreateActivityFromActivityTemplateInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId);

            var activity = await _activityManager.CreateAsync(Activity.Create(
                input.StartTime,
                input.EndTime,
                activityTemplate,
                currentUser
            ));

            foreach (var activityTemplateDescription in activityTemplate.Descriptions)
                switch (activityTemplateDescription.Type)
                {
                    case DescriptionType.Text:
                        await _descriptionManager.CreateAsync(TextDescription.Create(((TextDescription) activityTemplateDescription).Text, activity, currentUser.Id));
                        break;
                    case DescriptionType.ExternalImage:
                        await _descriptionManager.CreateAsync(ExternalImageDescription.Create(((ExternalImageDescription) activityTemplateDescription).Path, activity, currentUser.Id));
                        break;
                    case DescriptionType.InternalImage:
                        await _descriptionManager.CreateAsync(InternalImageDescription.Create(((InternalImageDescription) activityTemplateDescription).Image, activity, currentUser.Id));
                        break;
                    case DescriptionType.Youtube:
                        await _descriptionManager.CreateAsync(YoutubeDescription.Create(((YoutubeDescription) activityTemplateDescription).YoutubeId, activity, currentUser.Id));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return new EntityDto<Guid>(activity.Id);
        }

        [AbpAuthorize]
        public async Task UpdateActivity(UpdateActivityInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activity = await _activityManager.GetAsync(input.Id);
            var location = input.LocationId.HasValue ? await _locationManager.GetLocationAsync(input.LocationId.Value) : null;
            var tags = await _tagManager.GetTags(input.TagTexts);

            _activityManager.EditActivity(activity, input.Name, input.StartTime, input.EndTime, location, tags, currentUserId);
            _activityManager.EditDescriptions(activity, input.DescriptionIds, currentUserId);
        }

        [AbpAuthorize]
        public async Task RemoveActivity(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var activity = await _activityManager.GetAsync(input.Id);

            await _activityManager.RemoveAsync(activity, currentUserId);
        }

        private async Task InjectCoverDescriptionAsync(ICollection<ActivityListDto> activityTemplateListDtos)
        {
            var acitivtyTemplateIds = activityTemplateListDtos.Select(activity => activity.Id);

            var activityImageDescriptionDictionary = await _descriptionRepository.GetAll()
                .Where(d => d is ExternalImageDescription || d is InternalImageDescription)
                .Where(description => description.AbstractActivityId != null && acitivtyTemplateIds.Contains(description.AbstractActivityId.Value))
                .GroupBy(description => description.AbstractActivityId.Value)
                .Where(descriptionGroup => descriptionGroup.Any())
                .Select(descriptionGroup => new {descriptionGroup.Key, description = descriptionGroup.FirstOrDefault()})
                .ToDictionaryAsync(a => a.Key, a => a.description);

            var activityTextDescriptionDictionary = await _descriptionRepository.GetAll()
                .Where(d => d is TextDescription)
                .Where(description => description.AbstractActivityId != null && acitivtyTemplateIds.Contains(description.AbstractActivityId.Value))
                .GroupBy(description => description.AbstractActivityId.Value)
                .Where(descriptionGroup => descriptionGroup.Any())
                .Select(descriptionGroup => new {descriptionGroup.Key, description = descriptionGroup.FirstOrDefault()})
                .ToDictionaryAsync(a => a.Key, a => a.description);

            foreach (var activityTemplateListDto in activityTemplateListDtos)
            {
                var activityTemplateId = activityTemplateListDto.Id;

                if (activityImageDescriptionDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.CoverImageDescription = activityImageDescriptionDictionary[activityTemplateId].MapTo<DescriptionDto>();

                if (activityTextDescriptionDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.CoverTextDescription = activityTextDescriptionDictionary[activityTemplateId].MapTo<DescriptionDto>();
            }
        }

        private async Task InjectLikesAsync(ICollection<ActivityListDto> activityTemplateListDtos)
        {
            var activityTemplateIds = activityTemplateListDtos.Select(activity => activity.Id);

            var likesDictionary = await _ratingRepository.GetAll()
                .Where(rating => rating.AbstractActivityId != null && activityTemplateIds.Contains(rating.AbstractActivityId.Value))
                .GroupBy(rating => rating.AbstractActivityId)
                .Select(ratingGroup => new {ratingGroup.Key, Count = ratingGroup.LongCount(r => r.RatingStatus == RatingStatus.Like)})
                .ToDictionaryAsync(rating => rating.Key, ratings => ratings.Count);

            foreach (var activityTemplateListDto in activityTemplateListDtos)
            {
                var activityTemplateId = activityTemplateListDto.Id;

                if (likesDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.Likes = likesDictionary[activityTemplateId];
            }
        }

        private async Task InjectMyRatingStatusAsync(ICollection<ActivityListDto> activityTemplateListDtos)
        {
            var currentUserId = AbpSession.UserId;

            if (currentUserId.HasValue)
            {
                var activityTemplateIds = activityTemplateListDtos.Select(activity => activity.Id);

                var activityRatingStatusDictionary = await _ratingRepository.GetAll()
                    .Where(rating => rating.OwnerId == currentUserId.Value && rating.AbstractActivityId != null && activityTemplateIds.Contains(rating.AbstractActivityId.Value))
                    .GroupBy(rating => rating.AbstractActivityId.Value)
                    .Where(ratingGroup => ratingGroup.Any())
                    .Select(ratingGroup => new {ratingGroup.Key, ratingGroup.FirstOrDefault().RatingStatus})
                    .ToDictionaryAsync(ratingGroup => ratingGroup.Key, ratingGroup => ratingGroup.RatingStatus);

                foreach (var activityTemplateListDto in activityTemplateListDtos)
                {
                    var activityTemplateId = activityTemplateListDto.Id;

                    if (activityRatingStatusDictionary.ContainsKey(activityTemplateId))
                        activityTemplateListDto.MyRatingStatus = activityRatingStatusDictionary[activityTemplateId];
                }
            }
        }
    }
}