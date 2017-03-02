using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Locations.Managers
{
    public interface ILocationManager : IDomainService
    {
        Task<Location> GetLocationAsync(Guid id);

        Task<Location> CreateLocationAsync(Location location);
    }
}