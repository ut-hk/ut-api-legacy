using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities;
using UniTime.Comments.Dtos;
using UniTime.Comments.Managers;

namespace UniTime.Comments
{
    public interface ICommentAppService : IApplicationService
    {
        Task<GetCommentsOutput> GetComments();

        Task<EntityDto<long>> CreateComment(CreateCommentInput input);
    }

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

        public async Task<GetCommentsOutput> GetComments()
        {
            var comments = await _commentRepository.GetAllListAsync();

            return new GetCommentsOutput
            {
                Comments = comments.MapTo<List<CommentDto>>()
            };
        }

        public async Task<EntityDto<long>> CreateComment(CreateCommentInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            Comment comment = null;

            if (input.AbstractActivityId.HasValue)
            {
                var abstractActivity = await _abstractActivityRepository.FirstOrDefaultAsync(input.AbstractActivityId.Value);

                comment = await _commentManager.CreateCommentAsync(new AbstractActivityComment
                {
                    Content = input.Content,
                    AbstractActivity = abstractActivity,
                    AbstractActivityId = abstractActivity.Id,
                    Owner = currentUser,
                    OwnerId = currentUser.Id
                });
            }
            if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanRepository.FirstOrDefaultAsync(input.ActivityPlanId.Value);

                comment = await _commentManager.CreateCommentAsync(new ActivityPlanComment
                {
                    Content = input.Content,
                    ActivityPlan = activityPlan,
                    ActivityPlanId = activityPlan.Id,
                    Owner = currentUser,
                    OwnerId = currentUser.Id
                });
            }

            if (comment == null) throw new UserFriendlyException("Please provide either abstractActivityId or activityPlanId.");

            return new EntityDto<long>(comment.Id);
        }
    }

    public class CreateCommentInput
    {
        public string Content { get; set; }

        public Guid? AbstractActivityId { get; set; }

        public Guid? ActivityPlanId { get; set; }
    }

    public class GetCommentsOutput
    {
        public IReadOnlyList<CommentDto> Comments { get; set; }
    }
}