using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace UniTime.Locations.Dtos
{
    public class GetLocationsInput : IPagedResultRequest
    {
        public string QueryKeywords { get; set; }

        [Range(1, 10)]
        public int MaxResultCount { get; set; } = 10;

        public int SkipCount { get; set; } = 0;
    }
}