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

        /// <summary>
        ///     My Activities are the owned activities and the participated activities.
        /// </summary>
        /// <returns></returns>
        public async Task<GetMyActivitiesOutput> GetMyActivities()
        {
            var currentUser = await GetCurrentUserAsync();

            var myActivities = await _abstractActivityRepository.GetAll().OfType<Activity>()
                .Where(activity =>
                    activity.OwnerId == currentUser.Id ||
                    activity.Participants.Select(participant => participant.OwnerId).Contains(currentUser.Id)
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
                name: input.Name,
                description: input.Description,
                startTime: input.StartTime,
                endTime: input.EndTime,
                owner: currentUser
            ));

            return new EntityDto<Guid>(activity.Id);
        }
    }
}