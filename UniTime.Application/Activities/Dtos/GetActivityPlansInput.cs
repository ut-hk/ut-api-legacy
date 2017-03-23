using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace UniTime.Activities.Dtos
{
    public class GetActivityPlansInput : IPagedResultRequest
    {
        public string[] TagTexts { get; set; }

        public string QueryKeywords { get; set; }

        [Range(1, 20)]
        public int MaxResultCount { get; set; } = 10;

        public int SkipCount { get; set; } = 0;
    }
}