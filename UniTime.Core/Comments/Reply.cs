using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Comments
{
    public class Reply : FullAuditedEntity<long>, IHasOwner
    {
        protected Reply()
        {
        }

        public virtual string Content { get; set; }

        [ForeignKey(nameof(CommentId))]
        public virtual Comment Comment { get; set; }

        public virtual long CommentId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public virtual long OwnerId { get; set; }

        public static Reply Create(string content, Comment comment, User owner)
        {
            return new Reply
            {
                Content = content,
                Comment = comment,
                CommentId = comment.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}