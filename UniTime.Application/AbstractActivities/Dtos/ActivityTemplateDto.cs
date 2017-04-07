using System.Collections.Generic;
using Abp.AutoMapper;
using UniTime.Activities;

namespace UniTime.AbstractActivities.Dtos
{
    [AutoMapFrom(typeof(ActivityTemplate))]
    public class ActivityTemplateDto : AbstractActivityDto
    {
        public ICollection<ActivityTemplateReferenceTimeSlotDto> ReferenceTimeSlots { get; set; }
    }
}