﻿using System;
using System.Collections.Generic;
using Abp.AutoMapper;
using UniTime.Activities;

namespace UniTime.AbstractActivities.Dtos
{
    [AutoMapFrom(typeof(Activity))]
    public class ActivityDto : AbstractActivityDto
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public ICollection<ActivityParticipantDto> Participants { get; set; }
    }
}