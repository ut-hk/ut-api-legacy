using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.UI;
using UniTime.Activities.Managers;
using UniTime.Descriptions.Dtos;
using UniTime.Descriptions.Managers;
using UniTime.Files;
using UniTime.Files.Managers;

namespace UniTime.Descriptions
{
    [AbpAuthorize]
    public class DescriptionAppService : UniTimeAppServiceBase, IDescriptionAppService
    {
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IDescriptionManager _descriptionManager;
        private readonly IFileManager _fileManager;

        public DescriptionAppService(
            IDescriptionManager descriptionManager,
            IActivityPlanManager activityPlanManager,
            IFileManager fileManager)
        {
            _descriptionManager = descriptionManager;
            _activityPlanManager = activityPlanManager;
            _fileManager = fileManager;
        }

        public async Task<EntityDto<long>> CreateTextDescription(CreateTextDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId);

            var textDescription = await _descriptionManager.CreateAsync(TextDescription.Create(activityPlan, currentUserId));

            return new EntityDto<long>(textDescription.Id);
        }

        public async Task<EntityDto<long>> CreateExternalImageDescription(CreateExternalImageDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId);

            var externalImageDescription = await _descriptionManager.CreateAsync(ExternalImageDescription.Create(input.Path, activityPlan, currentUserId));

            return new EntityDto<long>(externalImageDescription.Id);
        }

        public async Task<EntityDto<long>> CreateInternalImageDescription(CreateInternalImageDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId);
            var image = await _fileManager.GetAsync(input.ImageId) as Image ?? throw new UserFriendlyException("Please give an image.");

            var internalImageDescription = await _descriptionManager.CreateAsync(InternalImageDescription.Create(image, activityPlan, currentUserId));

            return new EntityDto<long>(internalImageDescription.Id);
        }

        public async Task<EntityDto<long>> CreateYoutubeDescription(CreateYoutubeDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId);

            var internalImageDescription = await _descriptionManager.CreateAsync(YoutubeDescription.Create(input.YoutubeId, activityPlan, currentUserId));

            return new EntityDto<long>(internalImageDescription.Id);
        }

        public async Task UpdateDescription(UpdateDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();

            var textDescription = await _descriptionManager.GetAsync(input.Id);

            _descriptionManager.EditDescription(textDescription, input.HTMLClasses, currentUserId);
        }

        public async Task UpdateTextDescription(UpdateTextDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();

            var textDescription = await _descriptionManager.GetAsync(input.Id) as TextDescription;

            if (textDescription == null)
                throw new UserFriendlyException($"The text activity plan description with id = {input.Id} does not exist.");

            _descriptionManager.EditDescription(textDescription, input.HTMLClasses, currentUserId);
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