using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Locations.Managers
{
    public class LocationManager : ILocationManager
    {
        private readonly IRepository<Location, Guid> _locationRepository;

        public LocationManager(
            IRepository<Location, Guid> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Task<Location> GetLocationAsync(Guid id)
        {
            var location = _locationRepository.FirstOrDefaultAsync(id);

            if (location == null) throw new UserFriendlyException("The location with id = " + id + " does not exist.");

            return location;
        }

        public async Task<Location> CreateLocationAsync(Location location)
        {
            location.Id = await _locationRepository.InsertAndGetIdAsync(location);

            return location;
        }
    }
}