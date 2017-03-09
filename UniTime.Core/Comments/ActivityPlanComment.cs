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
        public virtual ActivityPlan ActivityPlan { get; set; }

        public virtual Guid ActivityPlanId { get; set; }

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