using System.Collections.Generic;

namespace UniTime.ActivityPlans.Dtos
{
    public class GetActivityPlansOutput
    {
        public ICollection<ActivityPlanListDto> ActivityPlans { get; set; }
    }
}