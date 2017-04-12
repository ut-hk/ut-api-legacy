using System;

namespace UniTime.AbstractActivities.Dtos
{
    public class CreateActivityFromActivityTemplateInput
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public Guid ActivityTemplateId { get; set; }
    }
}