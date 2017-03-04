using System.Collections.Generic;

namespace UniTime.Activities.Dtos
{
    public class GetMyActivitiesOutput
    {
        public IReadOnlyList<ActivityDto> MyActivities { get; set; }
    }
}