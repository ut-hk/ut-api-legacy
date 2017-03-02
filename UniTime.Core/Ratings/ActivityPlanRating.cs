using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;

namespace UniTime.Ratings
{
    public class ActivityPlanRating : Rating
    {
        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; set; }

        public virtual Guid ActivityPlanId { get; set; }
    }
}