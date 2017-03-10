using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace UniTime.Analysis
{
    public class RouteHistory : Entity<long>, IHasCreationTime
    {
        protected RouteHistory()
        {
        }

        [Required]
        public virtual string RouteName { get; protected set; }

        [Required]
        public virtual string Parameters { get; protected set; }

        public virtual string Referer { get; protected set; }

        public virtual Guest Guest { get; protected set; }

        public virtual Guid GuestId { get; protected set; }

        public virtual DateTime CreationTime { get; set; }

        public static RouteHistory Create(string routeName, string parameters, string referer, Guest guest)
        {
            return new RouteHistory
            {
                RouteName = routeName,
                Parameters = parameters,
                Referer = referer,
                Guest = guest,
                GuestId = guest.Id
            };
        }
    }
}