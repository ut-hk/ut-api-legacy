using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using Abp.Domain.Entities.Auditing;
using UniTime.Activities;

namespace UniTime.Locations
{
    public class Location : CreationAuditedEntity<Guid>
    {
        protected Location()
        {
        }

        public virtual string Name { get; protected set; }

        public virtual DbGeography Coordinate { get; protected set; }

        public virtual ICollection<AbstractActivity> Activities { get; protected set; }

        public static Location Create(string name, double longitude, double latitude)
        {
            var location = new Location
            {
                Name = name,
                Coordinate = DbGeography.FromText($"POINT({longitude} {latitude})")
            };

            return location;
        }
    }
}