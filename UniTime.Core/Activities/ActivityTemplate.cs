using System;
using System.Collections.Generic;
using UniTime.Users;

namespace UniTime.Activities
{
    public class ActivityTemplate : AbstractActivity
    {
        protected ActivityTemplate()
        {
        }

        public virtual DateTime? ReferenceStarTime { get; set; }

        public virtual DateTime? ReferenceEndTime { get; set; }

        public virtual ICollection<Activity> TemplatedActivities { get; set; }

        public virtual ICollection<ActivityPlanTimeSlot> MentionedTimeSlots { get; set; }

        public static ActivityTemplate Create(string name, string description, DateTime? referenceStartTime, DateTime? referenceEndTime, User owner)
        {
            var activityTemplate = new ActivityTemplate
            {
                Name = name,
                Description = description,
                ReferenceStarTime = referenceStartTime,
                ReferenceEndTime = referenceEndTime,
                Owner = owner,
                OwnerId = owner.Id
            };

            return activityTemplate;
        }
    }
}