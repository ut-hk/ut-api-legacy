using System;
using Abp.AutoMapper;

namespace UniTime.Activities.Dtos
{
    [AutoMapFrom(typeof(ActivityTemplateReferenceTimeSlot))]
    public class ActivityTemplateReferenceTimeSlotDto
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}