using System.Collections.Generic;

namespace UniTime.AbstractActivities.Dtos
{
    public class GetMyActivitiesOutput
    {
        public IReadOnlyList<ActivityListDto> MyActivities { get; set; }
    }
}