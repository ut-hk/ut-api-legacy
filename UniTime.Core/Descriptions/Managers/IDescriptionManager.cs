using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Descriptions.Managers
{
    public interface IDescriptionManager : IDomainService
    {
        Task<Description> GetDescriptionAsync(long id);
        Task<Description> CreateDescriptionAsync(Description description);
    }
}