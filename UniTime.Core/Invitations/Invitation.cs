using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Invitations.Enums;
using UniTime.Users;

namespace UniTime.Invitations
{
    public abstract class Invitation : AuditedEntity<Guid>, IHasOwner
    {
        public virtual string Content { get; protected set; }

        public virtual InvitationStatus Status { get; protected set; }

        [ForeignKey(nameof(InviteeId))]
        public virtual User Invitee { get; protected set; }

        public virtual long InviteeId { get; protected set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; protected set; }

        public virtual long OwnerId { get; protected set; }
    }
}