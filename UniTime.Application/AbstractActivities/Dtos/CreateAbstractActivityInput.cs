using System;

namespace UniTime.AbstractActivities.Dtos
{
    public class CreateAbstractActivityInput
    {
        public string Name { get; set; }

        public Guid? LocationId { get; set; }
    }
}