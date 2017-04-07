using System;
using Abp.Application.Services.Dto;

namespace UniTime.ActivityPlans.Dtos
{
    public class UpdateActivityPlanInput : EntityDto<Guid>
    {
        public string Name { get; set; }

        public long[] DescriptionIds { get; set; }

        public long[] TagIds { get; set; }
    }
}