﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Collections.Extensions;
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

        public virtual string HTMLClasses { get; protected set; }

        public virtual int Priority { get; protected set; }

        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; protected set; }

        public virtual Guid? ActivityPlanId { get; protected set; }

        public virtual void EditPriority(int priority)
        {
            Priority = priority;
        }

        public virtual void EditHTMLClasses(string[] htmlClasses, long editUserId)
        {
            HTMLClasses = htmlClasses
                .Select(htmlClass => htmlClass.Trim())
                .Where(htmlClass => !htmlClass.Contains(" "))
                .JoinAsString(" ");
        }
    }
}