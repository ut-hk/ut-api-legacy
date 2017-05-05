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
    }
}