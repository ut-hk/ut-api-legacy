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

        public virtual DateTime? StartTime { get; protected set; }

        public virtual DateTime? EndTime { get; protected set; }

        public virtual ICollection<ActivityParticipant> Participants { get; protected set; }

        public virtual ICollection<ActivityInvitation> Invitations { get; protected set; }

        [ForeignKey(nameof(ActivityTemplateId))]
        public virtual ActivityTemplate ActivityTemplate { get; protected set; }

        public virtual Guid? ActivityTemplateId { get; protected set; }

        public static Activity Create(string name, string description, DateTime? startTime, DateTime? endTime, User owner)
        {
            var activity = new Activity
            {
                Name = name,
                Description = description,
                StartTime = startTime,
                EndTime = endTime,
                Owner = owner,
                OwnerId = owner.Id
            };
            return activity;
        }
    }
}