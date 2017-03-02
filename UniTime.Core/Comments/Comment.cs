using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Comments
{
    public abstract class Comment : FullAuditedEntity<long>, IHasOwner
    {
        public virtual string Content { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public long OwnerId { get; set; }
    }
}