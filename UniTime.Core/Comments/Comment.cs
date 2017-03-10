using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Comments
{
    public abstract class Comment : FullAuditedEntity<long>, IHasOwner
    {
        public virtual string Content { get; protected set; }

        public virtual ICollection<Reply> Replies { get; protected set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; protected set; }

        public long OwnerId { get; protected set; }
    }
}