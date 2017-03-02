using System;

namespace UniTime.Activities.Dtos
{
    public class CreateActivityTemplateInput
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual DateTime? ReferenceStarTime { get; set; }

        public virtual DateTime? ReferenceEndTime { get; set; }
    }
}