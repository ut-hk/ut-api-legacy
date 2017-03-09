using System;
using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.ChatRooms
{
    public class ChatRoom : AuditedEntity<Guid>, IHasOwner
    {
        protected ChatRoom()
        {
        }

        public virtual string Name { get; set; }

        public virtual ICollection<User> Participants { get; set; }

        public virtual ICollection<ChatRoomMessage> Messages { get; set; }

        public virtual User Owner { get; set; }

        public virtual long OwnerId { get; set; }

        public static ChatRoom Create(string name, User owner, ICollection<User> participants)
        {
            return new ChatRoom
            {
                Name = name,
                Owner = owner,
                OwnerId = owner.Id,
                Participants = participants
            };
        }
    }
}