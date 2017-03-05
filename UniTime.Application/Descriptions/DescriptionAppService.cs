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
            var currentUser = await GetCurrentUserAsync();

            var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId);

            if (activityPlan.OwnerId != currentUser.Id) throw new UserFriendlyException($"You are not allowed to create a text description in this description with id = {activityPlan.Id}.");

            var textDescription = await _descriptionManager.CreateAsync(new TextActivityPlanDescription
            {
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id
            });

            return new EntityDto<long>(textDescription.Id);
        }

        public async Task UpdateTextDescription(UpdateTextDescriptionInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var textDescription = await _descriptionManager.GetAsync(input.Id) as TextActivityPlanDescription;

            if (textDescription == null) throw new UserFriendlyException("The text description with id = " + input.Id + " does not exist.");
            if (textDescription.ActivityPlan.OwnerId != currentUser.Id) throw new UserFriendlyException($"You are not allowed to update this description with id = {input.Id}.");

            textDescription.Text = input.Text;
        }
    }
}