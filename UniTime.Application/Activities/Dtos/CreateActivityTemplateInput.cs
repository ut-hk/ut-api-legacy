using System;

namespace UniTime.Activities.Dtos
{
    public class CreateActivityTemplateInput : CreateAbstractActivityInput
    {
        public virtual DateTime? ReferenceStarTime { get; set; }

        public virtual DateTime? ReferenceEndTime { get; set; }
    }
}