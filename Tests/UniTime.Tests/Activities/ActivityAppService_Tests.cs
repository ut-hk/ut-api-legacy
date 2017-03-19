using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.UI;
using Shouldly;
using UniTime.Activities;
using UniTime.Activities.Dtos;
using Xunit;

namespace UniTime.Tests.Activities
{
    public class ActivityAppService_Tests : UniTimeTestBase
    {
        public ActivityAppService_Tests()
        {
            _activityAppService = Resolve<IActivityAppService>();
        }

        private readonly IActivityAppService _activityAppService;

        [Fact]
        public async Task Should_Get_No_Result()
        {
            // Assert
            await Assert.ThrowsAsync<UserFriendlyException>(async () => await _activityAppService.GetActivity(new EntityDto<Guid>(Guid.Empty)));
        }

        [Fact]
        public async Task Should_Get_No_Results()
        {
            // Act
            var output = await _activityAppService.GetMyActivities();

            // Assert
            output.ShouldNotBe(null);
            output.MyActivities.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Should_Create_Activity()
        {
            const string name = "Hello World";
            const string description = "Happy";

            // Act
            var createActivityTemplateOutput = await _activityAppService.CreateActivity(new CreateActivityInput
            {
                Name = name,
                Description = description,
                LocationId = null,
                StartTime = new DateTime(2017, 3, 12, 2, 10, 0),
                EndTime = new DateTime(2017, 3, 12, 3, 10, 0)
            });

            // Assert
            createActivityTemplateOutput.ShouldNotBe(null);
            createActivityTemplateOutput.Id.ShouldBeOfType<Guid>();

            // Act
            var id = createActivityTemplateOutput.Id;
            var getActivityOutput = await _activityAppService.GetActivity(new EntityDto<Guid>(id));

            // Assert
            getActivityOutput.ShouldNotBe(null);
            getActivityOutput.Activity.ShouldNotBe(null);
            getActivityOutput.Activity.Id.ShouldBe(id);
            getActivityOutput.Activity.Name.ShouldBe(name);
            getActivityOutput.Activity.Description.ShouldBe(description);
            getActivityOutput.Activity.StartTime.ShouldBe(new DateTime(2017, 3, 12, 2, 10, 0));
            getActivityOutput.Activity.EndTime.ShouldBe(new DateTime(2017, 3, 12, 3, 10, 0));
        }
    }
}