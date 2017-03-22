using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using UniTime.Comments;
using UniTime.Descriptions;
using UniTime.Files;
using UniTime.Interfaces;
using UniTime.Locations;
using UniTime.Ratings;
using UniTime.Tags;
using UniTime.Users;

namespace UniTime.Activities
{
    public abstract class AbstractActivity : AuditedEntity<Guid>, IHasOwner
    {
        public virtual string Name { get; protected set; }

        public virtual ICollection<Description> Descriptions { get; protected set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; protected set; }

        public virtual Guid? LocationId { get; protected set; }

        public virtual ICollection<Tag> Tags { get; protected set; }

        public virtual ICollection<Rating> Ratings { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; protected set; }

        public virtual long OwnerId { get; protected set; }

        internal virtual void Edit(string name, Location location, ICollection<Tag> tags, long editUserId)
        {
            if (OwnerId != editUserId)
                throw new UserFriendlyException($"You are not allowed to update this activity with id = {Id}.");

            if (!string.IsNullOrWhiteSpace(name)) Name = name;

            if (location != null)
            {
                Location = location;
                LocationId = location.Id;
            }

            Tags.Clear();
            foreach (var tag in tags)
                Tags.Add(tag);
        }
    }
}