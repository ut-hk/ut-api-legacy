using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Comments.Managers
{
    public interface IReplyManager : IDomainService
    {
        Task<Reply> GetAsync(long id);

        Task<Reply> CreateAsync(Reply reply);
    }
}