using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.ChatRooms.Dtos;

namespace UniTime.ChatRooms
{
    public interface IChatRoomMessageAppService : IApplicationService
    {
        Task<GetChatRoomMessagesOutput> GetChatRoomMessages(GetChatRoomMessagesInput input);

        Task<EntityDto<long>> CreateTextChatRoomMessage(CreateTextChatRoomMessageInput input);
    }
}