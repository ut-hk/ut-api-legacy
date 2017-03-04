using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.ChatRooms.Managers
{
    public interface IChatRoomManager : IDomainService
    {
        Task<ChatRoom> GetAsync(Guid id);

        Task<ChatRoom> CreateAsync(ChatRoom chatRoom);
    }
}