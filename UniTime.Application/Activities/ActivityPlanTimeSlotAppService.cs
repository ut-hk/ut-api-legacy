using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.UI;
using UniTime.Activities.Dtos;
using UniTime.Activities.Managers;

namespace UniTime.Activities
{
    [AbpAuthorize]
    public class ActivityPlanTimeSlotAppService : UniTimeAppServiceBase, IActivityPlanTimeSlotAppService
    {
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IActivityPlanTimeSlotManager _activityPlanTimeSlotManager;
        private readonly IActivityTemplateManager _activityTemplateManager;

        public ActivityPlanTimeSlotAppService(
            IActivityPlanManager activityPlanManager,
            IActivityTemplateManager activityTemplateManager,
            IActivityPlanTimeSlotManager activityPlanTimeSlotManager)
        {
            _activityPlanManager = activityPlanManager;
            _activityTemplateManager = activityTemplateManager;
            _activityPlanTimeSlotManager = activityPlanTimeSlotManager;
        }

        public async Task<EntityDto<long>> CreateActivityPlanTimeSlot(CreateActivityPlanTimeSlotInput input)
        {
            var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId);
            var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId);

            var activityPlanTimeSlot = await _activityPlanTimeSlotManager.CreateAsync(ActivityPlanTimeSlot.Create(
                activityPlan,
                activityTemplate,
                input.StartTime,
                input.EndTime
            ));

            return new EntityDto<long>(activityPlanTimeSlot.Id);
        }

        public async Task UpdateActivityPlanTimeSlot(UpdateActivityPlanTimeSlotInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlanTimeSlot = await _activityPlanTimeSlotManager.GetAsync(input.Id);

            activityPlanTimeSlot.EditTimes(input.StartTime, input.EndTime, currentUserId);
        }

        public async Task RemoveActivityPlanTimeSlot(EntityDto<long> input)
        {
            var currentUser = await GetCurrentUserAsync();
            var activityPlanTimeSlot = await _activityPlanTimeSlotManager.GetAsync(input.Id);

            if (activityPlanTimeSlot.ActivityPlan.OwnerId != currentUser.Id)
                throw new UserFriendlyException("You are not allowed to change this activity plan.");

            await _activityPlanTimeSlotManager.RemoveAsync(activityPlanTimeSlot);
        }
    }
}