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
using UniTime.Activities;
using UniTime.Activities.Managers;
using UniTime.ActivityPlans.Dtos;
using UniTime.Tags.Managers;

namespace UniTime.ActivityPlans
{
    public class ActivityPlanAppService : UniTimeAppServiceBase, IActivityPlanAppService
    {
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly ITagManager _tagManager;
        private readonly IRepository<ActivityPlan, Guid> _activityPlanRepository;

        public ActivityPlanAppService(
            IRepository<ActivityPlan, Guid> activityPlanRepository,
            IActivityPlanManager activityPlanManager,
            ITagManager tagManager)
        {
            _activityPlanRepository = activityPlanRepository;
            _activityPlanManager = activityPlanManager;
            _tagManager = tagManager;
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
                .Include(activityPlan => activityPlan.Descriptions)
                .Include(activityPlan => activityPlan.Tags)
                .Include(activityPlan => activityPlan.TimeSlots)
                .Include(activityPlan => activityPlan.Comments)
                .Include(activityPlan => activityPlan.Ratings)
                .Include(activityPlan => activityPlan.Owner)

                // Optional Wheres
                .WhereIf(input.TagTexts != null && input.TagTexts.Length > 0,
                    activityPlan => input.TagTexts.Any(tagText => activityPlan.Tags.Select(tag => tag.Text).Contains(tagText)))
                .WhereIf(queryKeywords != null && queryKeywords.Length > 0,
                    activityPlan => queryKeywords.Any(queryKeyword => activityPlan.Name.Contains(queryKeyword)))
                .WhereIf(input.UserId.HasValue,
                    activityPlan => activityPlan.OwnerId == input.UserId.Value)

                // View Requirements
                .OrderByDescending(activityPlan => activityPlan.CreationTime)
                .PageBy(input)
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
            var tags = await _tagManager.GetTags(input.TagTexts);

            var activityPlan = await _activityPlanManager.CreateAsync(ActivityPlan.Create(
                input.Name, 
                tags,
                currentUser
            ));

            return new EntityDto<Guid>(activityPlan.Id);
        }

        [AbpAuthorize]
        public async Task UpdateActivityPlan(UpdateActivityPlanInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlan = await _activityPlanManager.GetAsync(input.Id);
            var tags = await _tagManager.GetTags(input.TagTexts);

            _activityPlanManager.EditActivityPlan(activityPlan, input.Name, tags, currentUserId);
            _activityPlanManager.EditDescriptions(activityPlan, input.DescriptionIds, currentUserId);
        }
    }
}