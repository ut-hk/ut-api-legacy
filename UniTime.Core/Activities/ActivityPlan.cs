﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Comments;
using UniTime.Descriptions;
using UniTime.Interfaces;
using UniTime.Ratings;
using UniTime.Tags;
using UniTime.Users;

namespace UniTime.Activities
{
    public class ActivityPlan : AuditedEntity<Guid>, IHasOwner
    {
        protected ActivityPlan()
        {
        }

        public virtual string Name { get; set; }

        public virtual ICollection<ActivityPlanDescription> Descriptions { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<ActivityPlanTimeSlot> TimeSlots { get; set; }

        public virtual ICollection<ActivityPlanComment> Comments { get; set; }

        public virtual ICollection<ActivityPlanRating> Ratings { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public virtual long OwnerId { get; set; }

        public static ActivityPlan Create(string name, User owner)
        {
            return new ActivityPlan
            {
                Name = name,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}