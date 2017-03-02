using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.ChatRooms.Managers
{
    public interface IChatRoomManager : IDomainService
    {
        Task<ChatRoom> GetChatRoomAsync(Guid id);
        Task<ChatRoom> CreateChatRoomAsync(ChatRoom chatRoom);
    }
}