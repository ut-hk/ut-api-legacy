using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.ChatRooms.Dtos;

namespace UniTime.ChatRooms
{
    public interface IChatRoomAppService : IApplicationService
    {
        Task<GetMyChatRoomsOutput> GetMyChatRooms();

        Task<EntityDto<Guid>> CreateChatRoom(CreateChatRoomInput input);
    }
}