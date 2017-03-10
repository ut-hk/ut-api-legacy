using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Comments.Managers
{
    public class ReplyManager : IReplyManager
    {
        private readonly IRepository<Reply, long> _replyRepository;

        public ReplyManager(
            IRepository<Reply, long> replyRepository)
        {
            _replyRepository = replyRepository;
        }

        public async Task<Reply> GetAsync(long id)
        {
            var reply = await _replyRepository.FirstOrDefaultAsync(id);

            if (reply == null)
                throw new UserFriendlyException("The comment with id = " + id + " does not exist.");

            return reply;
        }

        public async Task<Reply> CreateAsync(Reply reply)
        {
            reply.Id = await _replyRepository.InsertAndGetIdAsync(reply);

            return reply;
        }
    }
}