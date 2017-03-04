using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;

namespace UniTime.Locations.Dtos
{
    [AutoMapFrom(typeof(Location))]
    public class LocationDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public double CoordinateLongitude { get; set; }

        [JsonProperty(PropertyName = "Latitude")]
        public double CoordinateLatitude { get; set; }
    }
}