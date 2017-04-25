using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.AbstractActivities.Dtos;
using UniTime.Activities;

namespace UniTime.ActivityPlans.Dtos
{
    [AutoMapFrom(typeof(ActivityPlanTimeSlot))]
    public class ActivityPlanTimeSlotDto : EntityDto<long>
    {
        public ActivityTemplateListDto ActivityTemplate { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}