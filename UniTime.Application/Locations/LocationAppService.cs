using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using UniTime.Locations.Dtos;

namespace UniTime.Locations
{
    public class LocationAppService : ILocationAppService
    {
        public Task<GetLocationsOutput> GetLocations()
        {
            throw new NotImplementedException();
        }

        public Task<EntityDto<Guid>> CreateLocation(CreateLocationInput input)
        {
            throw new NotImplementedException();
        }
    }
}