using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace UniTime.AbstractActivities.Dtos
{
    public class GetActivityTemplatesInput : IPagedResultRequest
    {
        public string[] TagTexts { get; set; }

        public string QueryKeywords { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Range(-90.0, 90.0)]
        public double? Latitude { get; set; }

        [Range(-180.0, 180.0)]
        public double? Longitude { get; set; }

        public long? UserId { get; set; }

        [Range(1, 20)]
        public int MaxResultCount { get; set; } = 10;

        public int SkipCount { get; set; } = 0;
    }
}