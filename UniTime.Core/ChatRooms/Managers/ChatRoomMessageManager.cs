using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.ChatRooms.Managers
{
    public class ChatRoomMessageManager : IChatRoomMessageManager
    {
        private readonly IRepository<ChatRoomMessage, long> _chatRoomMessageRepository;

        public ChatRoomMessageManager(
            IRepository<ChatRoomMessage, long> chatRoomMessageRepository)
        {
            _chatRoomMessageRepository = chatRoomMessageRepository;
        }

        public async Task<ChatRoomMessage> GetAsync(long id)
        {
            var chatRoomMessage = await _chatRoomMessageRepository.FirstOrDefaultAsync(id);

            if (chatRoomMessage == null) throw new UserFriendlyException("The chat room message with id = " + id + " does not exist.");

            return chatRoomMessage;
        }

        public async Task<ChatRoomMessage> CreateAsync(ChatRoomMessage chatRoomMessage)
        {
            chatRoomMessage.Id = await _chatRoomMessageRepository.InsertAndGetIdAsync(chatRoomMessage);

            return chatRoomMessage;
        }
    }
}