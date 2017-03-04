using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Locations.Dtos;
using UniTime.Locations.Managers;

namespace UniTime.Locations
{
    public class LocationAppService : ILocationAppService
    {
        private readonly ILocationManager _locationManager;
        private readonly IRepository<Location, Guid> _locationRepository;

        public LocationAppService(
            IRepository<Location, Guid> locationRepository,
            ILocationManager locationManager)
        {
            _locationRepository = locationRepository;
            _locationManager = locationManager;
        }

        public async Task<GetLocationsOutput> GetLocations()
        {
            var locations = await _locationRepository.GetAllListAsync();

            return new GetLocationsOutput
            {
                Locations = locations.MapTo<List<LocationDto>>()
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateLocation(CreateLocationInput input)
        {
            var location = await _locationManager.CreateLocationAsync(Location.Create(input.Name, input.Longitude, input.Longitude));

            return new EntityDto<Guid>(location.Id);
        }
    }
}