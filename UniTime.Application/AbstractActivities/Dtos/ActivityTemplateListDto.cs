using System;

namespace UniTime.AbstractActivities.Dtos
{
    public class ActivityTemplateListDto : AbstractActivityListDto
    {
        public DateTime? StartTime { get; set; }
    }
}