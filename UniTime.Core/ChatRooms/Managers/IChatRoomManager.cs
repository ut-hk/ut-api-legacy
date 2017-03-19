using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using UniTime.Users;

namespace UniTime.ChatRooms.Managers
{
    public interface IChatRoomManager : IDomainService
    {
        Task<ChatRoom> GetAsync(Guid id);

        Task<ChatRoom> CreateAsync(ChatRoom chatRoom);

        void EditChatRoom(ChatRoom chatRoom, string name, long editUserId);
        void EditParticipants(ChatRoom chatRoom, List<User> participants, long editUserId);
    }
}