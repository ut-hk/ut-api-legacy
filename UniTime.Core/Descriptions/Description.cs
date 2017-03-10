using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using UniTime.Activities;
using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public abstract class Description : Entity<long>
    {
        [NotMapped]
        public virtual DescriptionType Type { get; }

        [NotMapped]
        public virtual string Content { get; }

        public virtual int Priority { get; protected set; }

        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; protected set; }

        public virtual Guid? ActivityPlanId { get; protected set; }


        public virtual void EditPriority(int priority)
        {
            Priority = priority;
        }
    }
}