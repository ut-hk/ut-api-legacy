using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.ChatRooms.Managers
{
    public interface IChatRoomMessageManager : IDomainService
    {
        Task<ChatRoomMessage> GetAsync(long id);
        Task<ChatRoomMessage> CreateAsync(ChatRoomMessage chatRoomMessage);
    }
}