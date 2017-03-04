using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace UniTime.Analysis
{
    public class RouteHistory : Entity<long>, IHasCreationTime
    {
        [Required]
        public virtual string RouteName { get; set; }

        [Required]
        public virtual string Parameters { get; set; }

        public virtual string Referer { get; set; }

        public virtual Guest Guest { get; set; }

        public virtual Guid GuestId { get; set; }

        public virtual DateTime CreationTime { get; set; }
    }
}