using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Comments.Managers
{
    public class CommentManager : ICommentManager
    {
        private readonly IRepository<Comment, Guid> _commentRepository;

        public CommentManager(
            IRepository<Comment, Guid> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> GetAsync(Guid id)
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