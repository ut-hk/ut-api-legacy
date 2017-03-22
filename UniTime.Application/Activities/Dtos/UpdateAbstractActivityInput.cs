﻿using System;
using Abp.Application.Services.Dto;

namespace UniTime.Activities.Dtos
{
    public class UpdateAbstractActivityInput : EntityDto<Guid>
    {
        public string Name { get; set; }

        public long[] DescriptionIds { get; set; }

        public long[] TagIds { get; set; }

        public Guid? LocationId { get; set; }
    }
}