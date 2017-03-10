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

            var textDescription = await _descriptionManager.CreateAsync(TextDescription.Create(activityPlan, currentUserId));

            return new EntityDto<long>(textDescription.Id);
        }

        public async Task UpdateTextDescription(UpdateTextDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();

            var textDescription = await _descriptionManager.GetAsync(input.Id) as TextDescription;

            if (textDescription == null)
                throw new UserFriendlyException($"The text activity plan description with id = {input.Id} does not exist.");

            _descriptionManager.EditTextDescription(textDescription, input.Text, currentUserId);
        }

        public async Task RemoveDescription(EntityDto<long> input)
        {
            var currentUserId = GetCurrentUserId();

            var activityPlanDescription = await _descriptionManager.GetAsync(input.Id);

            if (activityPlanDescription.ActivityPlan != null && currentUserId != activityPlanDescription.ActivityPlan.OwnerId)
                throw new UserFriendlyException($"You are not alloed to remove this description with id = {input.Id}.");

            await _descriptionManager.RemoveAsync(activityPlanDescription);
        }
    }
}