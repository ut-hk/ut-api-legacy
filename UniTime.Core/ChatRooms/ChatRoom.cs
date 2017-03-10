using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.ChatRooms
{
    public class ChatRoom : AuditedEntity<Guid>, IHasOwner
    {
        protected ChatRoom()
        {
        }

        public virtual string Name { get; protected set; }

        /// <summary>
        ///     Must contain the owner.
        /// </summary>
        public virtual ICollection<User> Participants { get; protected set; }

        public virtual ICollection<ChatRoomMessage> Messages { get; protected set; }

        public virtual User Owner { get; protected set; }

        public virtual long OwnerId { get; protected set; }

        public static ChatRoom Create(string name, User owner, ICollection<User> participants)
        {
            if (!participants.Select(participant => participant.Id).Contains(owner.Id))
                participants.Add(owner);

            return new ChatRoom
            {
                Name = name,
                Owner = owner,
                OwnerId = owner.Id,
                Participants = participants
            };
        }

        public void EditName(string name, User editUser)
        {
            if (Participants.Select(participant => participant.Id).Contains(editUser.Id))
                throw new UserFriendlyException("You are not allowed to change this chatRoom.");

            Name = name;
        }

        public void EditParticipants(ICollection<User> participants, User editUser)
        {
            if (Participants.Select(participant => participant.Id).Contains(editUser.Id))
                throw new UserFriendlyException("You are not allowed to change this chatRoom.");

            if (!participants.Select(participant => participant.Id).Contains(OwnerId))
                participants.Add(Owner);

            Participants.Clear();
            foreach (var participant in participants)
                Participants.Add(participant);
        }
    }
}