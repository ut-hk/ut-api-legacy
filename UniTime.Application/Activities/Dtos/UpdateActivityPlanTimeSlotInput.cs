using System;
using Abp.Application.Services.Dto;

namespace UniTime.Activities.Dtos
{
    public class UpdateActivityPlanTimeSlotInput : EntityDto<long>
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}