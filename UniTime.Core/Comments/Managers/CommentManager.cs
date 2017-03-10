using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Comments.Managers
{
    public class CommentManager : ICommentManager
    {
        private readonly IRepository<Comment, long> _commentRepository;

        public CommentManager(
            IRepository<Comment, long> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> GetAsync(long id)
        {
            var comment = await _commentRepository.FirstOrDefaultAsync(id);

            if (comment == null)
                throw new UserFriendlyException("The comment with id = " + id + " does not exist.");

            return comment;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            comment.Id = await _commentRepository.InsertAndGetIdAsync(comment);

            return comment;
        }
    }
}