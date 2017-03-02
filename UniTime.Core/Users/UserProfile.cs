using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Files;
using UniTime.Users.Enums;

namespace UniTime.Users
{
    public class UserProfile : AuditedEntity<long>
    {
        public virtual User User { get; set; }

        [ForeignKey(nameof(User))]
        public override long Id { get; set; }

        public virtual Gender Gender { get; set; } = Gender.NotProvided;

        public virtual DateTime Birthday { get; set; }

        [ForeignKey(nameof(CoverId))]
        public virtual Image Cover { get; set; }

        public virtual Guid? CoverId { get; set; }
    }
}