using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Locations.Dtos;

namespace UniTime.Locations
{
    public interface ILocationAppService : IApplicationService
    {
        Task<GetLocationsOutput> GetLocations();

        Task<EntityDto<Guid>> CreateLocation(CreateLocationInput input);
    }
}