using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Comments.Managers
{
    public interface IReplyManager : IDomainService
    {
        Task<Reply> GetReplyAsync(long id);

        Task<Reply> CreateReplyAsync(Reply reply);
    }
}