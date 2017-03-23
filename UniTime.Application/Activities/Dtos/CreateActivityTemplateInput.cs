using System;
using System.Collections.Generic;

namespace UniTime.Activities.Dtos
{
    public class CreateActivityTemplateInput : CreateAbstractActivityInput
    {
        public virtual ICollection<ActivityTemplateReferenceTimeSlotDto> ReferenceTimeSlots { get; set; }

        public virtual string ReferenceId { get; set; }
    }
}