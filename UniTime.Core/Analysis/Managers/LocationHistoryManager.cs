using System.Threading.Tasks;
using Abp.Domain.Repositories;
using UniTime.Locations;

namespace UniTime.Analysis.Managers
{
    public class LocationHistoryManager : ILocationHistoryManager
    {
        private readonly IRepository<LocationHistory, long> _locationHistoryRepository;

        public LocationHistoryManager(
            IRepository<LocationHistory, long> locationHistoryRepository)
        {
            _locationHistoryRepository = locationHistoryRepository;
        }

        public async Task<LocationHistory> CreateAsync(LocationHistory locationHistory)
        {
            locationHistory.Id = await _locationHistoryRepository.InsertAndGetIdAsync(locationHistory);

            return locationHistory;
        }
    }
}