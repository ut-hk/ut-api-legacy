using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using UniTime.Comments.Dtos;
using UniTime.Comments.Managers;

namespace UniTime.Comments
{
    [AbpAuthorize]
    public class ReplyAppService : UniTimeAppServiceBase, IReplyAppService
    {
        private readonly ICommentManager _commentManager;
        private readonly IReplyManager _replyManager;

        public ReplyAppService(
            ICommentManager commentManager,
            IReplyManager replyManager)
        {
            _commentManager = commentManager;
            _replyManager = replyManager;
        }

        public async Task<EntityDto<long>> CreateReply(CreateReplyInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var comment = await _commentManager.GetAsync(input.CommentId);

            var reply = await _replyManager.CreateAsync(Reply.Create(input.Content, comment, currentUser));

            return new EntityDto<long>(reply.Id);
        }
    }
}