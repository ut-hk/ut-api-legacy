using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UniTime.Activities.Dtos
{
    [AutoMapFrom(typeof(ActivityPlanTimeSlot))]
    public class ActivityPlanTimeSlotDto : EntityDto<long>
    {
        public ActivityTemplateDto ActivityTemplate { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}