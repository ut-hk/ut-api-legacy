using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
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
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual AbstractActivityLocation Location { get; set; }

        public virtual Guid? LocationId { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<AbstractActivityRating> Ratings { get; set; }

        public virtual ICollection<AbstractActivityComment> Comments { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public long OwnerId { get; set; }

        public virtual void EditName(string name)
        {
        }

        public virtual void EditDescription(string description)
        {
        }

        public virtual void EditLocation(Location location)
        {
        }
    }
}