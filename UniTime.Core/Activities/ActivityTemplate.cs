using System.Collections.Generic;
using UniTime.Users;

namespace UniTime.Activities
{
    public class ActivityTemplate : AbstractActivity
    {
        protected ActivityTemplate()
        {
        }

        public virtual string ReferenceId { get; protected set; }

        public virtual ICollection<ActivityTemplateReferenceTimeSlot> ReferenceTimeSlots { get; protected set; }

        public virtual ICollection<Activity> TemplatedActivities { get; protected set; }

        public virtual ICollection<ActivityPlanTimeSlot> MentionedTimeSlots { get; protected set; }

        public static ActivityTemplate Create(string name, string description, ICollection<ActivityTemplateReferenceTimeSlot> referenceTimeSlots, User owner)
        {
            var activityTemplate = new ActivityTemplate
            {
                Name = name,
                Description = description,
                ReferenceTimeSlots = referenceTimeSlots,
                Owner = owner,
                OwnerId = owner.Id
            };

            return activityTemplate;
        }
    }
}