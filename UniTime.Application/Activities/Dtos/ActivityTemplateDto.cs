using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UniTime.Activities.Dtos
{
    [AutoMapFrom(typeof(ActivityTemplate))]
    public class ActivityTemplateDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? ReferenceStarTime { get; set; }

        public DateTime? ReferenceEndTime { get; set; }
    }
}