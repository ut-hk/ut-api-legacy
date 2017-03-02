using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Comments.Managers
{
    public interface ICommentManager : IDomainService
    {
        Task<Comment> GetCommentAsync(long id);

        Task<Comment> CreateCommentAsync(Comment comment);
    }
}