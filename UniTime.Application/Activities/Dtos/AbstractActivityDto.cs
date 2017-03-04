using System;
using Abp.Application.Services.Dto;

namespace UniTime.Activities.Dtos
{
    public class AbstractActivityDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}