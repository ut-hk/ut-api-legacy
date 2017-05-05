using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Users;

namespace UniTime.ChatRooms.Managers
{
    public class ChatRoomManager : IChatRoomManager
    {
        private readonly IRepository<ChatRoom, Guid> _chatRoomRepository;

        public ChatRoomManager(
            IRepository<ChatRoom, Guid> chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task<ChatRoom> GetAsync(Guid id)
        {
            var chatRoom = await _chatRoomRepository.FirstOrDefaultAsync(id);

            if (chatRoom == null)
                throw new UserFriendlyException("The chat room with id = " + id + " does not exist.");

            return chatRoom;
        }

        public async Task<ChatRoom> CreateAsync(ChatRoom chatRoom)
        {
            chatRoom.Id = await _chatRoomRepository.InsertAndGetIdAsync(chatRoom);

            return chatRoom;
        }

        public void EditChatRoom(ChatRoom chatRoom, string name, long editUserId)
        {
            chatRoom.Edit(name, editUserId);
        }

        public void EditParticipants(ChatRoom chatRoom, List<User> participants, long editUserId)
        {
            chatRoom.EditParticipants(participants, editUserId);
        }
    }
}