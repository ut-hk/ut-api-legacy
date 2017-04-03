using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace UniTime.Activities.Dtos
{
    public class GetActivityTemplatesInput : IPagedResultRequest
    {
        public string[] TagTexts { get; set; }

        public string QueryKeywords { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public double? Distance { get; set; }

        public long? UserId { get; set; }

        [Range(1, 20)]
        public int MaxResultCount { get; set; } = 10;

        public int SkipCount { get; set; } = 0;
    }
}