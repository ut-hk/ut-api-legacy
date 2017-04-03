using System;

namespace UniTime.AbstractActivities.Dtos
{
    public class ActivityListDto : AbstractActivityListDto
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}