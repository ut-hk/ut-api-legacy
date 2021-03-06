﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Activities
{
    public class ActivityParticipant : Entity<long>, IHasOwner
    {
        protected ActivityParticipant()
        {
        }

        [ForeignKey(nameof(ActivityId))]
        public virtual Activity Activity { get; protected set; }

        public virtual Guid ActivityId { get; protected set; }

        public virtual User Owner { get; protected set; }

        public virtual long OwnerId { get; protected set; }

        public static ActivityParticipant Create(Activity actvity, User owner)
        {
            return new ActivityParticipant
            {
                Activity = actvity,
                ActivityId = actvity.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}