using System.Threading.Tasks;
using Abp.Domain.Services;
using UniTime.Locations;

namespace UniTime.Analysis.Managers
{
    public interface ILocationHistoryManager : IDomainService
    {
        Task<LocationHistory> CreateAsync(LocationHistory locationHistory);
    }
}