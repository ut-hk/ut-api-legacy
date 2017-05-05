using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
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

        public async Task<GetLocationsOutput> GetLocations(GetLocationsInput input)
        {
            var queryKeywords = input.QueryKeywords?.Split(' ').Where(queryKeyword => queryKeyword.Length > 0).ToArray();

            var locations = await _locationRepository.GetAll()
                .WhereIf(queryKeywords != null && queryKeywords.Length > 0, location => queryKeywords.Any(queryKeyword => location.Name.Contains(queryKeyword)))
                .OrderBy(location => location.Name)
                .PageBy(input)
                .ToListAsync();

            return new GetLocationsOutput
            {
                Locations = locations.MapTo<List<LocationDto>>()
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateLocation(CreateLocationInput input)
        {
            var location = await _locationManager.CreateLocationAsync(Location.Create(input.Name, input.Longitude, input.Latitude));

            return new EntityDto<Guid>(location.Id);
        }
    }
}