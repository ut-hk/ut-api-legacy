using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;
using UniTime.Users;

namespace UniTime.Comments
{
    public class ActivityPlanComment : Comment
    {
        protected ActivityPlanComment()
        {
        }

        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; protected set; }

        public virtual Guid ActivityPlanId { get; protected set; }

        public static ActivityPlanComment Create(string content, ActivityPlan activityPlan, User owner)
        {
            return new ActivityPlanComment
            {
                Content = content,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}