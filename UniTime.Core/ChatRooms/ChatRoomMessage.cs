using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.ChatRooms.Enums;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.ChatRooms
{
    public class ChatRoomMessage : CreationAuditedEntity<long>, IHasOwner
    {
        [NotMapped]
        public virtual ChatRoomMessageType Type { get; }

        [NotMapped]
        public virtual string Message { get; set; }

        [ForeignKey(nameof(ChatRoomId))]
        public virtual ChatRoom ChatRoom { get; set; }

        public virtual Guid ChatRoomId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public long OwnerId { get; set; }
    }
}