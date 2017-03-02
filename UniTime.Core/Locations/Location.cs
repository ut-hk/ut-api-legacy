using System;
using Abp.Domain.Entities.Auditing;

namespace UniTime.Locations
{
    public class Location : CreationAuditedEntity<Guid>
    {
        public virtual string Name { get; set; }
    }
}