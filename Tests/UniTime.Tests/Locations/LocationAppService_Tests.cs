using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using UniTime.Locations;
using UniTime.Locations.Dtos;
using Xunit;

namespace UniTime.Tests.Locations
{
    public class LocationAppService_Tests : UniTimeTestBase
    {
        public LocationAppService_Tests()
        {
            _locationAppService = Resolve<ILocationAppService>();
        }

        private readonly ILocationAppService _locationAppService;

        [Fact]
        public async Task Should_Get_No_Results()
        {
            // Act
            var getLocationsOutput = await _locationAppService.GetLocations(new GetLocationsInput());

            // Assert
            getLocationsOutput.ShouldNotBe(null);
            getLocationsOutput.Locations.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Should_Create_Location()
        {
            const double latitude = 0;
            const double longitude = 0;
            const string name = "Diamond Queen Location";

            // Act
            var createLocationOutput = await _locationAppService.CreateLocation(new CreateLocationInput
            {
                Latitude = latitude,
                Longitude = longitude,
                Name = name
            });

            // Assert
            createLocationOutput.ShouldNotBe(null);
            createLocationOutput.Id.ShouldNotBe(Guid.Empty);

            // Act
            var id = createLocationOutput.Id;
            var getLocationsOutput = await _locationAppService.GetLocations(new GetLocationsInput());

            // Assert
            getLocationsOutput.ShouldNotBe(null);
            getLocationsOutput.Locations.ShouldNotBe(null);
            getLocationsOutput.Locations.Count.ShouldBeGreaterThan(0);

            // Assert
            var locationDto = getLocationsOutput.Locations.First(l => l.Id == id);
            locationDto.Name.ShouldBe(name);

            // Latitude and Longitude are null, EffortDB doesnot support DbGeography
        }
    }
}