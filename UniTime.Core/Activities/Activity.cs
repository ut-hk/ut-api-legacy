using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Invitations;
using UniTime.Locations;
using UniTime.Tags;
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

        public static Activity Create(string name, DateTime? startTime, DateTime? endTime, Location location, ICollection<Tag> tags, User owner)
        {
            var actvitiy = new Activity
            {
                Name = name,
                Tags = tags,
                Owner = owner,
                OwnerId = owner.Id,
                StartTime = startTime,
                EndTime = endTime,
            };

            if (location != null)
            {
                actvitiy.Location = location;
                actvitiy.LocationId = location.Id;
            }

            return actvitiy;
        }

        public static Activity Create(DateTime? startTime, DateTime? endTime, ActivityTemplate activityTemplate, User owner)
        {
            var activity = new Activity
            {
                Name = activityTemplate.Name,
                Tags = activityTemplate.Tags,
                Owner = owner,
                OwnerId = owner.Id,
                StartTime = startTime,
                EndTime = endTime,
                ActivityTemplate = activityTemplate,
                ActivityTemplateId = activityTemplate.Id
            };

            return activity;
        }

        internal void Edit(DateTime? startTime, DateTime? endTime, long editUserId)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}