using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.ChatRooms.Enums;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.ChatRooms
{
    public abstract class ChatRoomMessage : CreationAuditedEntity<long>, IHasOwner
    {
        [NotMapped]
        public virtual ChatRoomMessageType Type { get; }

        [NotMapped]
        public virtual string Message { get; }

        [ForeignKey(nameof(ChatRoomId))]
        public virtual ChatRoom ChatRoom { get; protected set; }

        public virtual Guid ChatRoomId { get; protected set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; protected set; }

        public long OwnerId { get; protected set; }
    }
}