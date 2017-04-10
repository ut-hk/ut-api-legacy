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
        private readonly IActivityManager _activityManager;
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IActivityTemplateManager _activityTemplateManager;
        private readonly IDescriptionManager _descriptionManager;
        private readonly IFileManager _fileManager;

        public DescriptionAppService(IDescriptionManager descriptionManager,
            IActivityManager activityManager,
            IActivityTemplateManager activityTemplateManager,
            IActivityPlanManager activityPlanManager,
            IFileManager fileManager)
        {
            _descriptionManager = descriptionManager;
            _activityManager = activityManager;
            _activityTemplateManager = activityTemplateManager;
            _activityPlanManager = activityPlanManager;
            _fileManager = fileManager;
        }

        public async Task<EntityDto<long>> CreateTextDescription(CreateTextDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();

            Description textDescription;

            if (input.ActivityId.HasValue)
            {
                var activity = await _activityManager.GetAsync(input.ActivityId.Value);
                textDescription = await _descriptionManager.CreateAsync(TextDescription.Create(input.Text, activity, currentUserId));
            }
            else if (input.ActivityTemplateId.HasValue)
            {
                var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId.Value);
                textDescription = await _descriptionManager.CreateAsync(TextDescription.Create(input.Text, activityTemplate, currentUserId));
            }
            else if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);
                textDescription = await _descriptionManager.CreateAsync(TextDescription.Create(input.Text, activityPlan, currentUserId));
            }
            else
            {
                throw new UserFriendlyException("");
            }

            return new EntityDto<long>(textDescription.Id);
        }

        public async Task<EntityDto<long>> CreateExternalImageDescription(CreateExternalImageDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();

            Description externalImageDescription;

            if (input.ActivityId.HasValue)
            {
                var activity = await _activityManager.GetAsync(input.ActivityId.Value);
                externalImageDescription = await _descriptionManager.CreateAsync(ExternalImageDescription.Create(input.Path, activity, currentUserId));
            }
            else if (input.ActivityTemplateId.HasValue)
            {
                var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId.Value);
                externalImageDescription = await _descriptionManager.CreateAsync(ExternalImageDescription.Create(input.Path, activityTemplate, currentUserId));
            }
            else if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);
                externalImageDescription = await _descriptionManager.CreateAsync(ExternalImageDescription.Create(input.Path, activityPlan, currentUserId));
            }
            else
            {
                throw new UserFriendlyException("");
            }

            return new EntityDto<long>(externalImageDescription.Id);
        }

        public async Task<EntityDto<long>> CreateInternalImageDescription(CreateInternalImageDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();
            var image = await _fileManager.GetAsync(input.ImageId) as Image ?? throw new UserFriendlyException("Please give an image.");

            Description internalImageDescription;

            if (input.ActivityId.HasValue)
            {
                var activity = await _activityManager.GetAsync(input.ActivityId.Value);
                internalImageDescription = await _descriptionManager.CreateAsync(InternalImageDescription.Create(image, activity, currentUserId));
            }
            else if (input.ActivityTemplateId.HasValue)
            {
                var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId.Value);
                internalImageDescription = await _descriptionManager.CreateAsync(InternalImageDescription.Create(image, activityTemplate, currentUserId));
            }
            else if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);
                internalImageDescription = await _descriptionManager.CreateAsync(InternalImageDescription.Create(image, activityPlan, currentUserId));
            }
            else
            {
                throw new UserFriendlyException("");
            }

            return new EntityDto<long>(internalImageDescription.Id);
        }

        public async Task<EntityDto<long>> CreateYoutubeDescription(CreateYoutubeDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();

            Description youtubeDescription;

            if (input.ActivityId.HasValue)
            {
                var activity = await _activityManager.GetAsync(input.ActivityId.Value);
                youtubeDescription = await _descriptionManager.CreateAsync(YoutubeDescription.Create(input.YoutubeId, activity, currentUserId));
            }
            else if (input.ActivityTemplateId.HasValue)
            {
                var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId.Value);
                youtubeDescription = await _descriptionManager.CreateAsync(YoutubeDescription.Create(input.YoutubeId, activityTemplate, currentUserId));
            }
            else if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);
                youtubeDescription = await _descriptionManager.CreateAsync(YoutubeDescription.Create(input.YoutubeId, activityPlan, currentUserId));
            }
            else
            {
                throw new UserFriendlyException("");
            }

            return new EntityDto<long>(youtubeDescription.Id);
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
                throw new UserFriendlyException($"The description with id = {input.Id} does not exist.");

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