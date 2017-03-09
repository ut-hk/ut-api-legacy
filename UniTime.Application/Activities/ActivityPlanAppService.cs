using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
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

        public async Task<GetActivityPlansOutput> GetActivityPlans(GetActivityPlansInput input)
        {
            var queryKeywords = input.QueryKeywords?.Split(' ').Where(queryKeyword => queryKeyword.Length > 0).ToArray();

            var activityPlans = await _activityPlanRepository.GetAll()
                .WhereIf(input.TagTexts != null && input.TagTexts.Length > 0, activityPlan => input.TagTexts.Any(tagText => activityPlan.Tags.Select(tag => tag.Text).Contains(tagText)))
                .WhereIf(queryKeywords != null && queryKeywords.Length > 0, activityPlan => queryKeywords.Any(queryKeyword => activityPlan.Name.Contains(queryKeyword)))
                .ToListAsync();

            return new GetActivityPlansOutput
            {
                ActivityPlans = activityPlans.MapTo<List<ActivityPlanDto>>()
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivityPlan(CreateActivityPlanInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var activityPlan = await _activityPlanManager.CreateAsync(ActivityPlan.Create(
                input.Name,
                currentUser
            ));

            return new EntityDto<Guid>(activityPlan.Id);
        }

        [AbpAuthorize]
        public async Task UpdateActivityPlan(UpdateActivityPlanInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlan = await _activityPlanManager.GetAsync(input.Id);

            if (activityPlan.OwnerId != currentUserId)
                throw new UserFriendlyException("You are not allowed to update this activity plan.");

            activityPlan.Name = input.Name;

            var activityPlanDescriptions = activityPlan.Descriptions;

            foreach (var activityPlanDescription in activityPlanDescriptions)
                for (var i = 0; i < input.DescriptionIds.Length; i++)
                    // Give priority by the input array.
                    if (input.DescriptionIds[i] == activityPlanDescription.Id)
                        activityPlanDescription.Priority = i;
        }
    }
}