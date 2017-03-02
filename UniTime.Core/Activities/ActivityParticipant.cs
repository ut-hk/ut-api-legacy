using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Activities
{
    public class ActivityParticipant : Entity<long>, IHasOwner
    {
        [ForeignKey(nameof(ActivityId))]
        public virtual Activity Activity { get; set; }

        public virtual Guid ActivityId { get; set; }

        public virtual User Owner { get; set; }

        public virtual long OwnerId { get; set; }
    }
}