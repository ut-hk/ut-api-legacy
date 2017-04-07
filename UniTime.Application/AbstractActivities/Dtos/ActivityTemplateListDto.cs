using System;
using Abp.AutoMapper;
using UniTime.Activities;

namespace UniTime.AbstractActivities.Dtos
{
    [AutoMapFrom(typeof(ActivityTemplate))]
    public class ActivityTemplateListDto : AbstractActivityListDto
    {
        public DateTime? StartTime { get; set; }
    }
}