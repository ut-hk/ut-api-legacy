using System;
using Abp.Application.Services.Dto;

namespace UniTime.AbstractActivities.Dtos
{
    public class UpdateAbstractActivityInput : EntityDto<Guid>
    {
        public string Name { get; set; }

        public long[] DescriptionIds { get; set; }

        public string[] TagTexts { get; set; }

        public Guid? LocationId { get; set; }
    }
}