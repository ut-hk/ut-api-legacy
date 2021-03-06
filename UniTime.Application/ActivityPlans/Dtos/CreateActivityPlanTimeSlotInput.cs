using System;

namespace UniTime.ActivityPlans.Dtos
{
    public class CreateActivityPlanTimeSlotInput
    {
        public Guid ActivityPlanId { get; set; }

        public Guid ActivityTemplateId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}