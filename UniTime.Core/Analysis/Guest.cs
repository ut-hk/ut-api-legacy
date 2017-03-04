using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using UniTime.Users;

namespace UniTime.Analysis
{
    public class Guest : Entity<Guid>, IHasCreationTime
    {
        public virtual ICollection<RouteHistory> RouteHistories { get; set; }

        public virtual User Owner { get; set; }

        public virtual long? OwnerId { get; set; }

        public virtual DateTime CreationTime { get; set; }
    }
}