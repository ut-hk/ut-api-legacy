using System.Collections.Generic;
using Abp.AutoMapper;

namespace UniTime.Activities.Dtos
{
    [AutoMapFrom(typeof(ActivityTemplate))]
    public class ActivityTemplateDto : AbstractActivityDto
    {
        public ICollection<ActivityTemplateReferenceTimeSlotDto> ReferenceTimeSlots { get; set; }
    }
}