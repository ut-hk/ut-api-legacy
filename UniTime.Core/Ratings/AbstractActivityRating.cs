using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;
using UniTime.Ratings.Enums;
using UniTime.Users;

namespace UniTime.Ratings
{
    public class AbstractActivityRating : Rating
    {
        protected AbstractActivityRating()
        {
        }

        [ForeignKey(nameof(AbstractActivityId))]
        public virtual AbstractActivity AbstractActivity { get; protected set; }

        public virtual Guid AbstractActivityId { get; protected set; }

        public static AbstractActivityRating Create(RatingStatus ratingStatus, AbstractActivity abstractActivity, User owner)
        {
            return new AbstractActivityRating
            {
                RatingStatus = ratingStatus,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}