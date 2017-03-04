using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Analysis.Managers
{
    public interface IRouteHistoryManager : IDomainService
    {
        Task<RouteHistory> CreateAsync(RouteHistory routeHistory);
    }
}