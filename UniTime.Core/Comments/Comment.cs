using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Activities;
using UniTime.Comments.Enums;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Comments
{
    public abstract class Comment : FullAuditedEntity<Guid>, IHasOwner
    {
        [NotMapped]
        public virtual string Content { get; }

        [NotMapped]
        public virtual CommentType Type { get; set; }

        public virtual ICollection<Reply> Replies { get; protected set; }

        [ForeignKey(nameof(AbstractActivityId))]
        public virtual AbstractActivity AbstractActivity { get; protected set; }

        public virtual Guid? AbstractActivityId { get; protected set; }

        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; protected set; }

        public virtual Guid? ActivityPlanId { get; protected set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; protected set; }

        public long OwnerId { get; protected set; }
    }
}