using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using UniTime.Activities;

namespace UniTime.Categories
{
    public class Category : FullAuditedEntity<long>
    {
        public virtual string Name { get; set; }

        public virtual ICollection<ActivityPlan> ActivityPlans { get; set; }

        public virtual ICollection<ActivityTemplate> ActivityTemplates { get; set; }
    }
}