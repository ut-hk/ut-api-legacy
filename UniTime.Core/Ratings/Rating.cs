using System;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Ratings.Enums;
using UniTime.Users;

namespace UniTime.Ratings
{
    public abstract class Rating : AuditedEntity<Guid>, IHasOwner
    {
        public virtual RatingStatus RatingStatus { get; set; }

        public virtual User Owner { get; set; }

        public virtual long OwnerId { get; set; }
    }
}