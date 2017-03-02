using System;
using System.Collections.Generic;

namespace UniTime.Activities
{
    public class ActivityTemplate : AbstractActivity
    {
        public virtual DateTime? ReferenceStarTime { get; set; }

        public virtual DateTime? ReferenceEndTime { get; set; }

        public virtual ICollection<Activity> TemplatedActivities { get; set; }

        public virtual ICollection<ActivityPlanTimeSlot> MentionedTimeSlots { get; set; }

        public virtual Activity ConvertToActivity()
        {
            return new Activity();
        }
    }
}