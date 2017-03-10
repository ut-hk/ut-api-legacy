using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using UniTime.Activities;

namespace UniTime.Categories
{
    public class Category : FullAuditedEntity<long>
    {
        protected Category()
        {
        }

        public virtual string Name { get; protected set; }

        public virtual ICollection<ActivityPlan> ActivityPlans { get; protected set; }

        public virtual ICollection<ActivityTemplate> ActivityTemplates { get; protected set; }
    }
}