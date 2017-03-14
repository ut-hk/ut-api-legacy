using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.UI;
using UniTime.Activities.Managers;
using UniTime.Comments.Dtos;
using UniTime.Comments.Managers;
using UniTime.Files;
using UniTime.Files.Managers;

namespace UniTime.Comments
{
    [AbpAuthorize]
    public class CommentAppService : UniTimeAppServiceBase, ICommentAppService
    {
        private readonly IActivityManager _activityManager;
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IActivityTemplateManager _activityTemplateManager;
        private readonly ICommentManager _commentManager;
        private readonly IFileManager _fileManager;

        public CommentAppService(
            ICommentManager commentManager,
            IFileManager fileManager,
            IActivityManager activityManager,
            IActivityPlanManager activityPlanManager,
            IActivityTemplateManager activityTemplateManager)
        {
            _commentManager = commentManager;
            _fileManager = fileManager;
            _activityManager = activityManager;
            _activityPlanManager = activityPlanManager;
            _activityTemplateManager = activityTemplateManager;
        }

        public async Task<EntityDto<Guid>> CreateTextComment(CreateTextCommentInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            Comment textComment = null;

            if (input.ActivityId.HasValue)
            {
                var activity = await _activityManager.GetAsync(input.ActivityId.Value);

                textComment = await _commentManager.CreateAsync(TextComment.Create(input.Content, activity, currentUser));
            }
            else if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);

                textComment = await _commentManager.CreateAsync(TextComment.Create(input.Content, activityPlan, currentUser));
            }
            else if (input.ActivityTemplateId.HasValue)
            {
                var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId.Value);

                textComment = await _commentManager.CreateAsync(TextComment.Create(input.Content, activityTemplate, currentUser));
            }
            else
            {
                throw new UserFriendlyException("Please provide either abstractActivityId or activityPlanId.");
            }

            return new EntityDto<Guid>(textComment.Id);
        }

        public async Task<EntityDto<Guid>> CreateInternalImageComment(CreateInternalImageCommentInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var image = await _fileManager.GetAsync(input.ImageId) as Image ?? throw new UserFriendlyException("Please give an image.");

            Comment internalImageComment = null;

            if (input.ActivityId.HasValue)
            {
                var activity = await _activityManager.GetAsync(input.ActivityId.Value);

                internalImageComment = await _commentManager.CreateAsync(InternalImageComment.Create(image, activity, currentUser));
            }
            else if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);

                internalImageComment = await _commentManager.CreateAsync(InternalImageComment.Create(image, activityPlan, currentUser));
            }
            else if (input.ActivityTemplateId.HasValue)
            {
                var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId.Value);

                internalImageComment = await _commentManager.CreateAsync(InternalImageComment.Create(image, activityTemplate, currentUser));
            }
            else
            {
                throw new UserFriendlyException("Please provide either activityId or activityPlanId or activityTemplateId.");
            }

            return new EntityDto<Guid>(internalImageComment.Id);
        }
    }
}