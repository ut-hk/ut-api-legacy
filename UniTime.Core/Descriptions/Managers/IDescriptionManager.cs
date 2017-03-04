using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Descriptions.Managers
{
    public interface IDescriptionManager : IDomainService
    {
        Task<Description> GetAsync(long id);
        Task<Description> CreateAsync(Description description);
    }
}