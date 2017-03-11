using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace UniTime.Users
{
    public class Track : Entity<long>
    {
        protected Track()
        {
        }

        [ForeignKey(nameof(FromId))]
        public virtual User From { get; protected set; }

        public virtual long FromId { get; protected set; }

        [ForeignKey(nameof(ToId))]
        public virtual User To { get; protected set; }

        public virtual long ToId { get; protected set; }

        public static Track Create(
            User from,
            User to)
        {
            return new Track
            {
                From = from,
                FromId = from.Id,
                To = to,
                ToId = to.Id
            };
        }
    }
}