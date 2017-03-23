using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities;
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
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IDescriptionManager _descriptionManager;
        private readonly IFileManager _fileManager;

        public DescriptionAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IDescriptionManager descriptionManager,
            IActivityPlanManager activityPlanManager,
            IFileManager fileManager)
        {
            _abstractActivityRepository = abstractActivityRepository;
            _descriptionManager = descriptionManager;
            _activityPlanManager = activityPlanManager;
            _fileManager = fileManager;
        }

        public async Task<EntityDto<long>> CreateTextDescription(CreateTextDescriptionInput input)
        {
            var currentUserId = GetCurrentUserId();
            Description textDescription;

            if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);
                textDescription = await _descriptionManager.CreateAsync(TextDescription.Create(input.Text, activityPlan, currentUserId));
            }
            else if (input.AbstractActivityId.HasValue)
            {
                var abstractActvitiy = await _abstractActivityRepository.GetAsync(input.AbstractActivityId.Value);
                textDescription = await _descriptionManager.CreateAsync(TextDescription.Create(input.Text, abstractActvitiy, currentUserId));
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

            if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);
                externalImageDescription = await _descriptionManager.CreateAsync(ExternalImageDescription.Create(input.Path, activityPlan, currentUserId));
            }
            else if (input.AbstractActivityId.HasValue)
            {
                var abstractActvitiy = await _abstractActivityRepository.GetAsync(input.AbstractActivityId.Value);
                externalImageDescription = await _descriptionManager.CreateAsync(ExternalImageDescription.Create(input.Path, abstractActvitiy, currentUserId));
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

            if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);
                internalImageDescription = await _descriptionManager.CreateAsync(InternalImageDescription.Create(image, activityPlan, currentUserId));
            }
            else if (input.AbstractActivityId.HasValue)
            {
                var abstractActvitiy = await _abstractActivityRepository.GetAsync(input.AbstractActivityId.Value);
                internalImageDescription = await _descriptionManager.CreateAsync(InternalImageDescription.Create(image, abstractActvitiy, currentUserId));
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

            Description internalImageDescription;

            if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);
                internalImageDescription = await _descriptionManager.CreateAsync(YoutubeDescription.Create(input.YoutubeId, activityPlan, currentUserId));
            }
            else if (input.AbstractActivityId.HasValue)
            {
                var abstractActvitiy = await _abstractActivityRepository.GetAsync(input.AbstractActivityId.Value);
                internalImageDescription = await _descriptionManager.CreateAsync(YoutubeDescription.Create(input.YoutubeId, abstractActvitiy, currentUserId));
            }
            else
            {
                throw new UserFriendlyException("");
            }

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