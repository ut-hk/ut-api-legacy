using System;
using Abp.AutoMapper;

namespace UniTime.Activities.Dtos
{
    [AutoMapFrom(typeof(ActivityTemplate))]
    public class ActivityTemplateDto : AbstractActivityDto
    {
        public DateTime? ReferenceStarTime { get; set; }

        public DateTime? ReferenceEndTime { get; set; }
    }
}