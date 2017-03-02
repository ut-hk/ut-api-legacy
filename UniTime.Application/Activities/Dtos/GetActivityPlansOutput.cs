using System.Collections.Generic;

namespace UniTime.Activities.Dtos
{
    public class GetActivityPlansOutput
    {
        public ICollection<ActivityPlanDto> ActivityPlans { get; set; }
    }
}