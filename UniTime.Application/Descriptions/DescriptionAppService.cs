using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.UI;
using UniTime.Activities.Managers;
using UniTime.Descriptions.Dtos;
using UniTime.Descriptions.Managers;

namespace UniTime.Descriptions
{
    [AbpAuthorize]
    public class DescriptionAppService : UniTimeAppServiceBase, IDescriptionAppService
    {
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IDescriptionManager _descriptionManager;

        public DescriptionAppService(
            IDescriptionManager descriptionManager,
            IActivityPlanManager activityPlanManager)
        {
            _descriptionManager = descriptionManager;
            _activityPlanManager = activityPlanManager;
        }

        public async Task<EntityDto<long>> CreateTextDescription(CreateTextDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();

            var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId);

            if (activityPlan.OwnerId != currentUserId) throw new UserFriendlyException($"You are not allowed to create a text description in this description with id = {activityPlan.Id}.");

            var textDescription = await _descriptionManager.CreateAsync(TextActivityPlanDescription.Create(activityPlan));

            return new EntityDto<long>(textDescription.Id);
        }

        public async Task UpdateTextDescription(UpdateTextDescriptionInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var textActivityPlanDescription = await _descriptionManager.GetAsync(input.Id) as TextActivityPlanDescription;

            if (textActivityPlanDescription == null) throw new UserFriendlyException("The text activity plan description with id = " + input.Id + " does not exist.");
            if (textActivityPlanDescription.ActivityPlan.OwnerId != currentUser.Id) throw new UserFriendlyException($"You are not allowed to update this description with id = {input.Id}.");

            textActivityPlanDescription.Text = input.Text;
        }

        public async Task RemoveDescription(EntityDto<long> input)
        {
            var currentUser = await GetCurrentUserAsync();

            var activityPlanDescription = await _descriptionManager.GetAsync(input.Id) as ActivityPlanDescription;

            if (activityPlanDescription == null) throw new UserFriendlyException("The activity plan description with id = {input.Id} does not exist.");
            if (currentUser.Id != activityPlanDescription.ActivityPlan.OwnerId) throw new UserFriendlyException("You are not alloed to remove this description");

            await _descriptionManager.RemoveAsync(activityPlanDescription);
        }
    }
}