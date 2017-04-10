using System.Collections.Generic;
using UniTime.Locations;
using UniTime.Tags;
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

        public static ActivityTemplate Create(string name, Location location, ICollection<ActivityTemplateReferenceTimeSlot> referenceTimeSlots, ICollection<Tag> tags, User owner, string referenceId)
        {
            var activityTemplate = new ActivityTemplate
            {
                Name = name,
                Tags = tags,
                ReferenceId = referenceId,
                ReferenceTimeSlots = referenceTimeSlots,
                Owner = owner,
                OwnerId = owner.Id
            };

            if (location != null)
            {
                activityTemplate.Location = location;
                activityTemplate.LocationId = location.Id;
            }

            return activityTemplate;
        }

        internal void Edit(ICollection<ActivityTemplateReferenceTimeSlot> referenceTimeSlots, long editUserId)
        {
            ReferenceTimeSlots.Clear();
            foreach (var referenceTimeSlot in referenceTimeSlots)
                ReferenceTimeSlots.Add(referenceTimeSlot);
        }
    }
}