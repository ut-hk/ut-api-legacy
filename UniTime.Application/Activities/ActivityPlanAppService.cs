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
using UniTime.Activities.Dtos;
using UniTime.Activities.Managers;
using UniTime.Tags;

namespace UniTime.Activities
{
    public class ActivityPlanAppService : UniTimeAppServiceBase, IActivityPlanAppService
    {
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IRepository<ActivityPlan, Guid> _activityPlanRepository;
        private readonly IRepository<Tag, long> _tagRepository;

        public ActivityPlanAppService(
            IRepository<ActivityPlan, Guid> activityPlanRepository,
            IRepository<Tag, long> tagRepository,
            IActivityPlanManager activityPlanManager)
        {
            _activityPlanRepository = activityPlanRepository;
            _tagRepository = tagRepository;
            _activityPlanManager = activityPlanManager;
        }

        public async Task<GetActivityPlanOutput> GetActivityPlan(EntityDto<Guid> input)
        {
            var activityPlan = await _activityPlanManager.GetAsync(input.Id);

            return new GetActivityPlanOutput
            {
                ActivityPlan = activityPlan.MapTo<ActivityPlanDto>()
            };
        }

        public async Task<GetActivityPlansOutput> GetActivityPlans(GetActivityPlansInput input)
        {
            var queryKeywords = input.QueryKeywords?.Split(' ').Where(queryKeyword => queryKeyword.Length > 0).ToArray();

            var activityPlans = await _activityPlanRepository.GetAll()
                .Include(activityPlan => activityPlan.Tags)
                .WhereIf(input.TagTexts != null && input.TagTexts.Length > 0,
                    activityPlan => input.TagTexts.Any(tagText => activityPlan.Tags.Select(tag => tag.Text).Contains(tagText)))
                .WhereIf(queryKeywords != null && queryKeywords.Length > 0,
                    activityPlan => queryKeywords.Any(queryKeyword => activityPlan.Name.Contains(queryKeyword)))
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
            var tags = await _tagRepository.GetAllListAsync(tag => input.TagIds.Contains(tag.Id));

            _activityPlanManager.EditActivityPlan(activityPlan, input.Name, tags, currentUserId);
            _activityPlanManager.EditDescriptions(activityPlan, input.DescriptionIds, currentUserId);
        }
    }
}