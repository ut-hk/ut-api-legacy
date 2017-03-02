using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UniTime.Locations.Dtos
{
    [AutoMapFrom(typeof(Location))]
    public class LocationDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}