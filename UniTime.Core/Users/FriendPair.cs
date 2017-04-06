using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace UniTime.Users
{
    public class FriendPair : Entity<long>
    {
        protected FriendPair()
        {
        }

        [ForeignKey(nameof(LeftId))]
        public virtual User Left { get; protected set; }

        public virtual long LeftId { get; protected set; }

        [ForeignKey(nameof(RightId))]
        public virtual User Right { get; protected set; }

        public virtual long RightId { get; protected set; }

        public static FriendPair Create(
            User left,
            User right)
        {
            return new FriendPair
            {
                Left = left,
                LeftId = left.Id,
                Right = right,
                RightId = right.Id
            };
        }
    }
}