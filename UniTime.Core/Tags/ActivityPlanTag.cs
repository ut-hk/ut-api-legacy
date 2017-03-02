using System.Collections.Generic;
using UniTime.Activities;

namespace UniTime.Tags
{
    public class ActivityPlanTag : Tag
    {
        public virtual ICollection<ActivityPlan> ActivityPlans { get; set; }
    }
}