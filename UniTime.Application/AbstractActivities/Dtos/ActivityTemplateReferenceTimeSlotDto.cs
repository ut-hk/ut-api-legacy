using System;
using Abp.AutoMapper;
using UniTime.Activities;

namespace UniTime.AbstractActivities.Dtos
{
    [AutoMapFrom(typeof(ActivityTemplateReferenceTimeSlot))]
    public class ActivityTemplateReferenceTimeSlotDto
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}