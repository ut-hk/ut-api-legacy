using System;
using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using UniTime.Users;

namespace UniTime.ChatRooms
{
    public class ChatRoom : AuditedEntity<Guid>
    {
        public virtual ICollection<User> Participants { get; set; }

        public virtual ICollection<ChatRoomMessage> Messages { get; set; }
    }
}