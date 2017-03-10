using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;
using UniTime.Ratings.Enums;
using UniTime.Users;

namespace UniTime.Ratings
{
    public class ActivityPlanRating : Rating
    {
        protected ActivityPlanRating()
        {
        }

        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; protected set; }

        public virtual Guid ActivityPlanId { get; protected set; }

        public static ActivityPlanRating Create(RatingStatus ratingStatus, ActivityPlan activityPlan, User owner)
        {
            return new ActivityPlanRating
            {
                RatingStatus = ratingStatus,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}