using System;
using Abp.AutoMapper;
using UniTime.Activities;

namespace UniTime.AbstractActivities.Dtos
{
    [AutoMapFrom(typeof(Activity))]
    public class ActivityListDto : AbstractActivityListDto
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public Guid? ActivityTemplateId { get; set; }
    }
}