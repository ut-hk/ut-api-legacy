using System;

namespace UniTime.Activities.Dtos
{
    public class UpdateActivityInput : UpdateAbstractActivityInput
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}