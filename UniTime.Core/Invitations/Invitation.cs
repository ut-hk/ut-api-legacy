using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Invitations.Enums;
using UniTime.Users;

namespace UniTime.Invitations
{
    public abstract class Invitation : AuditedEntity<Guid>,IHasOwner
    {
        public virtual string Content { get; set; }

        public virtual InvitationStatus Status { get; set; }

        [ForeignKey(nameof(InviteeId))]
        public virtual User Invitee { get; set; }

        public virtual long InviteeId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public virtual long OwnerId { get; set; }
    }
}