using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using UniTime.Comments;
using UniTime.Descriptions;
using UniTime.Interfaces;
using UniTime.Ratings;
using UniTime.Tags;
using UniTime.Users;

namespace UniTime.Activities
{
    public class ActivityPlan : FullAuditedEntity<Guid>, IHasOwner
    {
        protected ActivityPlan()
        {
        }

        public virtual string Name { get; protected set; }

        public virtual ICollection<Description> Descriptions { get; protected set; }

        public virtual ICollection<Tag> Tags { get; protected set; }

        public virtual ICollection<ActivityPlanTimeSlot> TimeSlots { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        public virtual ICollection<Rating> Ratings { get; protected set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; protected set; }

        public virtual long OwnerId { get; protected set; }

        public static ActivityPlan Create(string name, ICollection<Tag> tags, User owner)
        {
            return new ActivityPlan
            {
                Name = name,
                Tags = tags,
                Owner = owner,
                OwnerId = owner.Id
            };
        }

        internal void Edit(string name, ICollection<Tag> tags, long editUserId)
        {
            if (OwnerId != editUserId)
                throw new UserFriendlyException($"You are not allowed to update this activity plan with id = {Id}.");

            Name = name;

            Tags.Clear();
            foreach (var tag in tags)
                Tags.Add(tag);
        }
    }
}