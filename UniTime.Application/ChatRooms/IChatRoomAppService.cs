using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.ChatRooms.Dtos;

namespace UniTime.ChatRooms
{
    public interface IChatRoomAppService : IApplicationService
    {
        /// <summary>
        ///     My Chat Rooms are the participated chat rooms.
        /// </summary>
        /// <returns></returns>
        Task<GetMyChatRoomsOutput> GetMyChatRooms();

        Task<EntityDto<Guid>> CreateChatRoom(CreateChatRoomInput input);

        Task UpdateChatRoom(UpdateChatRoomInput input);
    }
}