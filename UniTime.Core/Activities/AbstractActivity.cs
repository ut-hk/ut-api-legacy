using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using UniTime.Comments;
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

        public virtual string Description { get; protected set; }

        [ForeignKey(nameof(LocationId))]
        public virtual AbstractActivityLocation Location { get; protected set; }

        public virtual Guid? LocationId { get; protected set; }

        public virtual ICollection<Tag> Tags { get; protected set; }

        public virtual ICollection<AbstractActivityRating> Ratings { get; protected set; }

        public virtual ICollection<AbstractActivityComment> Comments { get; protected set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; protected set; }

        public long OwnerId { get; protected set; }

        public virtual void Edit(string name, string description, long editUserId)
        {
            if (OwnerId != editUserId) throw new UserFriendlyException($"You are not allowed to update this activity with id = {Id}.");

            Name = name;
            Description = description;
        }
    }
}