using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;

namespace UniTime.Descriptions
{
    public abstract class ActivityPlanDescription : Description
    {
        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; protected set; }

        public virtual Guid ActivityPlanId { get; protected set; }
    }
}