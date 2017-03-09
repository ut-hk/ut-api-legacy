using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

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

            if (chatRoom == null) throw new UserFriendlyException("The chat room with id = " + id + " does not exist.");

            return chatRoom;
        }

        public async Task<ChatRoom> CreateAsync(ChatRoom chatRoom)
        {
            chatRoom.Id = await _chatRoomRepository.InsertAndGetIdAsync(chatRoom);

            return chatRoom;
        }
    }
}