using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.ChatRooms.Managers
{
    public interface IChatRoomMessageManager : IDomainService
    {
        Task<ChatRoomMessage> GetChatRoomMessageAsync(long id);
        Task<ChatRoomMessage> CreateChatRoomMessageAsync(ChatRoomMessage chatRoomMessage);
    }
}