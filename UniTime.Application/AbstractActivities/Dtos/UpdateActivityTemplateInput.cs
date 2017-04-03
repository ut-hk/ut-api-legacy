using System.Collections.Generic;

namespace UniTime.AbstractActivities.Dtos
{
    public class UpdateActivityTemplateInput : UpdateAbstractActivityInput
    {
        public virtual ICollection<ActivityTemplateReferenceTimeSlotDto> ReferenceTimeSlots { get; set; }
    }
}