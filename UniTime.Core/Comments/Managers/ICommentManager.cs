using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Comments.Managers
{
    public interface ICommentManager : IDomainService
    {
        Task<Comment> GetAsync(long id);

        Task<Comment> CreateAsync(Comment comment);
    }
}