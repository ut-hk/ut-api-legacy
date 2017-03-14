using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Activities.Dtos;
using UniTime.Activities.Managers;

namespace UniTime.Activities
{
    [AbpAuthorize]
    public class ActivityAppService : UniTimeAppServiceBase, IActivityAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IActivityManager _activityManager;

        public ActivityAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IActivityManager activityManager)
        {
            _abstractActivityRepository = abstractActivityRepository;
            _activityManager = activityManager;
        }

        public async Task<GetActivityOutput> GetActivity(EntityDto<Guid> input)
        {
            var activity = await _activityManager.GetAsync(input.Id);

            return new GetActivityOutput
            {
                Activity = activity.MapTo<ActivityDto>()
            };
        }

        public async Task<GetMyActivitiesOutput> GetMyActivities()
        {
            var currentUserId = GetCurrentUserId();

            var myActivities = await _abstractActivityRepository.GetAll().OfType<Activity>()
                .Where(activity =>
                    activity.OwnerId == currentUserId ||
                    activity.Participants.Select(participant => participant.OwnerId).Contains(currentUserId)
                )
                .ToListAsync();

            return new GetMyActivitiesOutput
            {
                MyActivities = myActivities.MapTo<List<ActivityDto>>()
            };
        }

        public async Task<EntityDto<Guid>> CreateActivity(CreateActivityInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var activity = await _activityManager.CreateAsync(Activity.Create(
                input.Name,
                input.Description,
                input.StartTime,
                input.EndTime,
                currentUser
            ));

            return new EntityDto<Guid>(activity.Id);
        }

        public async Task UpdateActivity(UpdateAbstractActivityInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activity = await _activityManager.GetAsync(input.Id);

            _activityManager.EditActivity(activity, input.Name, input.Description, currentUserId);
        }
    }
}