using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.AbstractActivities.Dtos;
using UniTime.Activities;
using UniTime.Activities.Managers;
using UniTime.Locations.Managers;
using UniTime.Tags;

namespace UniTime.AbstractActivities
{
    public class ActivityAppService : UniTimeAppServiceBase, IActivityAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IActivityManager _activityManager;
        private readonly ILocationManager _locationManager;
        private readonly IRepository<Tag, long> _tagRepository;

        public ActivityAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IRepository<Tag, long> tagRepository,
            IActivityManager activityManager,
            ILocationManager locationManager)
        {
            _abstractActivityRepository = abstractActivityRepository;
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

            return new GetActivityOutput
            {
                Activity = activity.MapTo<ActivityDto>()
            };
        }

        public async Task<GetActivitiesOutput> GetActivities(GetActivitiesInput input)
        {
            var activities = await _abstractActivityRepository.GetAll()
                .OfType<Activity>()
                .Include(activity => activity.Descriptions)
                .Include(activity => activity.Location)
                .Include(activity => activity.Tags)
                .Include(activity => activity.Owner)
                .Where(activity =>
                    activity.OwnerId == input.UserId
                )
                .OrderBy(activity => activity.StartTime)
                .ToListAsync();

            return new GetActivitiesOutput
            {
                Activities = activities.MapTo<List<ActivityListDto>>()
            };
        }

        [AbpAuthorize]
        public async Task<GetMyActivitiesOutput> GetMyActivities()
        {
            var currentUserId = GetCurrentUserId();

            var myActivities = await _abstractActivityRepository.GetAll()
                .OfType<Activity>()
                .Include(activity => activity.Descriptions)
                .Include(activity => activity.Location)
                .Include(activity => activity.Tags)
                .Include(activity => activity.Ratings)
                .Include(activity => activity.Comments)
                .Include(activity => activity.Owner)
                .Include(activity => activity.Owner.Profile)
                .Include(activity => activity.Participants)
                .Where(activity =>
                    activity.OwnerId == currentUserId ||
                    activity.Participants.Select(participant => participant.OwnerId).Contains(currentUserId)
                )
                .OrderBy(activity => activity.StartTime)
                .ToListAsync();

            return new GetMyActivitiesOutput
            {
                MyActivities = myActivities.MapTo<List<ActivityListDto>>()
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
    }
}