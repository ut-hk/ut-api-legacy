using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using UniTime.Users;

namespace UniTime.Analysis
{
    public class Guest : Entity<Guid>, IHasCreationTime
    {
        protected Guest()
        {
        }

        public virtual ICollection<RouteHistory> RouteHistories { get; set; }

        public virtual User Owner { get; set; }

        public virtual long? OwnerId { get; set; }

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