using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace UniTime.Activities
{
    public class ActivityPlanTimeSlot : Entity<long>
    {
        [Required]
        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; set; }

        public virtual Guid ActivityPlanId { get; set; }

        [Required]
        [ForeignKey(nameof(ActivityTemplateId))]
        public virtual ActivityTemplate ActivityTemplate { get; set; }

        public virtual Guid ActivityTemplateId { get; set; }

        public virtual DateTime? StartTime { get; set; }

        public virtual DateTime? EndTime { get; set; }
    }
}