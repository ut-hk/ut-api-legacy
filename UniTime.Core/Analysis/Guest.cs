using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Analysis
{
    public class Guest : Entity<Guid>, IHasCreationTime
    {
        protected Guest()
        {
        }

        public virtual ICollection<RouteHistory> RouteHistories { get; protected set; }

        public virtual User Owner { get; protected set; }

        public virtual long? OwnerId { get; protected set; }

        public virtual DateTime CreationTime { get; set; }

        public static Guest Create(long? ownerId)
        {
            return new Guest
            {
                OwnerId = ownerId
            };
        }
    }
}