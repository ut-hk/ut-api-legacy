using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Activities.Dtos;
using UniTime.Activities.Managers;

namespace UniTime.Activities
{
    public class ActivityPlanAppService : UniTimeAppServiceBase, IActivityPlanAppService
    {
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IRepository<ActivityPlan, Guid> _activityPlanRepository;

        public ActivityPlanAppService(
            IRepository<ActivityPlan, Guid> activityPlanRepository,
            IActivityPlanManager activityPlanManager)
        {
            _activityPlanRepository = activityPlanRepository;
            _activityPlanManager = activityPlanManager;
        }

        public async Task<GetActivityPlansOutput> GetActivityPlans()
        {
            var activityPlans = await _activityPlanRepository.GetAllListAsync();

            return new GetActivityPlansOutput
            {
                ActivityPlans = activityPlans.MapTo<List<ActivityPlanDto>>()
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivityPlan(CreateActivityPlanInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var activityPlan = await _activityPlanManager.CreateAsync(new ActivityPlan
            {
                Name = input.Name,
                Owner = currentUser,
                OwnerId = currentUser.Id
            });

            return new EntityDto<Guid>(activityPlan.Id);
        }
    }
}