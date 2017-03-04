using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Invitations;
using UniTime.Users;

namespace UniTime.Activities
{
    public class Activity : AbstractActivity
    {
        protected Activity()
        {
        }

        public virtual DateTime? StarTime { get; set; }

        public virtual DateTime? EndTime { get; set; }

        public virtual ICollection<ActivityParticipant> Participants { get; set; }

        public virtual ICollection<ActivityInvitation> Invitations { get; set; }

        [ForeignKey(nameof(ActivityTemplateId))]
        public virtual ActivityTemplate ActivityTemplate { get; set; }

        public virtual Guid? ActivityTemplateId { get; set; }

        public static Activity Create(string name, string description, DateTime? startTime, DateTime? endTime, User owner)
        {
            var activity = new Activity
            {
                Name = name,
                Description = description,
                StarTime = startTime,
                EndTime = endTime,
                Owner = owner,
                OwnerId = owner.Id
            };
            return activity;
        }
    }
}