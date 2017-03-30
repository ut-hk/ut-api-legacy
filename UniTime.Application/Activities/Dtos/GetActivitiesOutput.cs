using System.Collections.Generic;

namespace UniTime.Activities.Dtos
{
    public class GetActivitiesOutput
    {
        public IReadOnlyList<ActivityDto> Activities { get; set; }
    }
}