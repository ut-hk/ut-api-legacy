using System;
using System.Data.Entity.Spatial;
using Abp.Domain.Entities.Auditing;

namespace UniTime.Locations
{
    public class Location : CreationAuditedEntity<Guid>
    {
        protected Location() { }

        public virtual string Name { get; set; }

        public virtual DbGeography Coordinate { get; set; }

        public static Location Create(string name, double longitude, double latitude)
        {
            var location = new Location()
            {
                Name = name,
                Coordinate = DbGeography.FromText($"POINT({longitude} {latitude})")
            };

            return location;
        }
    }
}