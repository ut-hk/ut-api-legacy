using System.Collections.Generic;

namespace UniTime.Locations.Dtos
{
    public class GetLocationsOutput
    {
        public IReadOnlyList<LocationDto> Locations { get; set; }
    }
}