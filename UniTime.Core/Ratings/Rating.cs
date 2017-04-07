using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using UniTime.Activities;
using UniTime.Interfaces;
using UniTime.Ratings.Enums;
using UniTime.Users;

namespace UniTime.Ratings
{
    public class Rating : AuditedEntity<long>, IHasOwner
    {
        public virtual RatingStatus RatingStatus { get; protected set; }

        [ForeignKey(nameof(AbstractActivityId))]
        public virtual AbstractActivity AbstractActivity { get; protected set; }

        public virtual Guid? AbstractActivityId { get; protected set; }

        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; protected set; }

        public virtual Guid? ActivityPlanId { get; protected set; }

        public virtual User Owner { get; protected set; }

        public virtual long OwnerId { get; protected set; }

        public static Rating Create(RatingStatus ratingStatus, AbstractActivity abstractActivity, User owner)
        {
            return new Rating
            {
                RatingStatus = ratingStatus,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }

        public static Rating Create(RatingStatus ratingStatus, ActivityPlan activityPlan, User owner)
        {
            return new Rating
            {
                RatingStatus = ratingStatus,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }

        internal void EditRating(RatingStatus ratingStatus, long editUserId)
        {
            if (OwnerId != editUserId)
                throw new UserFriendlyException("You are not allowed to change this rating.");

            RatingStatus = ratingStatus;
        }
    }
}