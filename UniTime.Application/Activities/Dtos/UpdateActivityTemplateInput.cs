using System.Collections.Generic;

namespace UniTime.Activities.Dtos
{
    public class UpdateActivityTemplateInput : UpdateAbstractActivityInput
    {
        public virtual ICollection<ActivityTemplateReferenceTimeSlotDto> ReferenceTimeSlots { get; set; }
    }
}