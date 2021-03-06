﻿using System;

namespace UniTime.AbstractActivities.Dtos
{
    public class CreateActivityInput : CreateAbstractActivityInput
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}