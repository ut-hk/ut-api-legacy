﻿using System;

namespace UniTime.Activities.Dtos
{
    public class CreateAbstractActivityInput
    {
        public string Name { get; set; }

        public Guid? LocationId { get; set; }
    }
}