using System.Collections.Generic;

namespace UniTime.ActivityPlans.Dtos
{
    public class GetActivityPlansOutput
    {
        public ICollection<ActivityPlanDto> ActivityPlans { get; set; }
    }
}