﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.UI;
using Shouldly;
using UniTime.AbstractActivities;
using UniTime.AbstractActivities.Dtos;
using UniTime.Activities;
using Xunit;

namespace UniTime.Tests.Activities
{
    public class ActivityTemplateAppService_Tests : UniTimeTestBase
    {
        public ActivityTemplateAppService_Tests()
        {
            _activityTemplateAppService = Resolve<IActivityTemplateAppService>();
        }

        private readonly IActivityTemplateAppService _activityTemplateAppService;

        [Fact]
        public async Task Should_Get_No_Result()
        {
            // Assert
            await Assert.ThrowsAsync<UserFriendlyException>(async () => await _activityTemplateAppService.GetActivityTemplate(new EntityDto<Guid>(Guid.Empty)));
        }

        [Fact]
        public async Task Should_Create_Activity_Template()
        {
            const string name = "Hello World";
            var startTime = new DateTime(2017, 3, 12, 2, 10, 0);
            var endTime = new DateTime(2017, 3, 12, 2, 10, 0);

            // Act
            var createActivityTemplateOutput = await _activityTemplateAppService.CreateActivityTemplate(new CreateActivityTemplateInput
            {
                Name = name,
                LocationId = null,
                ReferenceTimeSlots = new List<ActivityTemplateReferenceTimeSlotDto>
                {
                    new ActivityTemplateReferenceTimeSlotDto
                    {
                        StartTime = startTime,
                        EndTime = endTime
                    }
                }
            });

            // Assert
            createActivityTemplateOutput.ShouldNotBe(null);
            createActivityTemplateOutput.Id.ShouldBeOfType<Guid>();

            // Act
            var id = createActivityTemplateOutput.Id;
            var getActivityTemplateOutput = await _activityTemplateAppService.GetActivityTemplate(new EntityDto<Guid>(id));

            // Assert
            getActivityTemplateOutput.ShouldNotBe(null);
            getActivityTemplateOutput.ActivityTemplate.ShouldNotBe(null);
            getActivityTemplateOutput.ActivityTemplate.Id.ShouldBe(id);
            getActivityTemplateOutput.ActivityTemplate.Name.ShouldBe(name);
            getActivityTemplateOutput.ActivityTemplate.ReferenceTimeSlots.Count.ShouldBe(1);
            getActivityTemplateOutput.ActivityTemplate.ReferenceTimeSlots.First().StartTime.ShouldBe(startTime);
            getActivityTemplateOutput.ActivityTemplate.ReferenceTimeSlots.First().EndTime.ShouldBe(endTime);
        }
    }
}