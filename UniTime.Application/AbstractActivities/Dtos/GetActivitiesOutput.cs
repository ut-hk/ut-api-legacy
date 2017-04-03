using System.Collections.Generic;

namespace UniTime.AbstractActivities.Dtos
{
    public class GetActivitiesOutput
    {
        public IReadOnlyList<ActivityListDto> Activities { get; set; }
    }
}