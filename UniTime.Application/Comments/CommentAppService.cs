using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities;
using UniTime.Comments.Dtos;
using UniTime.Comments.Managers;

namespace UniTime.Comments
{
    [AbpAuthorize]
    public class CommentAppService : UniTimeAppServiceBase, ICommentAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IRepository<ActivityPlan, Guid> _activityPlanRepository;
        private readonly ICommentManager _commentManager;
        private readonly IRepository<Comment, long> _commentRepository;

        public CommentAppService(
            IRepository<Comment, long> commentRepository,
            ICommentManager commentManager,
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IRepository<ActivityPlan, Guid> activityPlanRepository)
        {
            _commentRepository = commentRepository;
            _commentManager = commentManager;
            _abstractActivityRepository = abstractActivityRepository;
            _activityPlanRepository = activityPlanRepository;
        }

        public async Task<EntityDto<long>> CreateComment(CreateCommentInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            Comment comment = null;

            if (input.AbstractActivityId.HasValue)
            {
                var abstractActivity = await _abstractActivityRepository.FirstOrDefaultAsync(input.AbstractActivityId.Value);

                comment = await _commentManager.CreateAsync(AbstractActivityComment.Create(input.Content, abstractActivity, currentUser));
            }
            if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanRepository.FirstOrDefaultAsync(input.ActivityPlanId.Value);

                comment = await _commentManager.CreateAsync(ActivityPlanComment.Create(input.Content, activityPlan, currentUser));
            }

            if (comment == null) throw new UserFriendlyException("Please provide either abstractActivityId or activityPlanId.");

            return new EntityDto<long>(comment.Id);
        }
    }
}