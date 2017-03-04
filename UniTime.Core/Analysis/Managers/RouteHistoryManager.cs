using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace UniTime.Analysis.Managers
{
    public class RouteHistoryManager : IRouteHistoryManager
    {
        private readonly IRepository<RouteHistory, long> _routeHistoryManager;

        public RouteHistoryManager(
            IRepository<RouteHistory, long> routeHistoryManager)
        {
            _routeHistoryManager = routeHistoryManager;
        }

        public async Task<RouteHistory> CreateAsync(RouteHistory routeHistory)
        {
            routeHistory.Id = await _routeHistoryManager.InsertAndGetIdAsync(routeHistory);

            return routeHistory;
        }
    }
}