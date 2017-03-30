using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.UI;

namespace UniTime.Analysis
{
    public class LocationHistory : Entity<long>, IHasCreationTime
    {
        protected LocationHistory()
        {
        }

        [Required]
        public virtual double Longitude { get; protected set; }

        [Required]
        public virtual double Latitude { get; protected set; }

        public virtual Guest Guest { get; protected set; }

        public virtual Guid GuestId { get; protected set; }

        public virtual DateTime CreationTime { get; set; }

        public static LocationHistory Create(double longitude, double latitude, Guest guest, long? createUserId)
        {
            if (guest.OwnerId != createUserId)
                throw new UserFriendlyException("You are not allowed to create this location history.");

            return new LocationHistory
            {
                Longitude = longitude,
                Latitude = latitude,
                Guest = guest,
                GuestId = guest.Id
            };
        }
    }
}