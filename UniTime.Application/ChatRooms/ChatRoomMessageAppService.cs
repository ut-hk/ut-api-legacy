using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.ChatRooms.Dtos;
using UniTime.ChatRooms.Managers;

namespace UniTime.ChatRooms
{
    [AbpAuthorize]
    public class ChatRoomMessageAppService : UniTimeAppServiceBase, IChatRoomMessageAppService
    {
        private readonly IChatRoomManager _chatRoomManager;
        private readonly IChatRoomMessageManager _chatRoomMessageManager;
        private readonly IRepository<ChatRoomMessage, long> _chatRoomMessageRepository;

        public ChatRoomMessageAppService(
            IChatRoomManager chatRoomManager,
            IChatRoomMessageManager chatRoomMessageManager,
            IRepository<ChatRoomMessage, long> chatRoomMessageRepository)
        {
            _chatRoomManager = chatRoomManager;
            _chatRoomMessageManager = chatRoomMessageManager;
            _chatRoomMessageRepository = chatRoomMessageRepository;
        }

        public async Task<GetChatRoomMessagesOutput> GetChatRoomMessages(GetChatRoomMessagesInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var chatRoom = await _chatRoomManager.GetAsync(input.ChatRoomId);

            if (!chatRoom.Participants.Select(participant => participant.Id).Contains(currentUser.Id))
                throw new UserFriendlyException("You are not allowed to view this chat room.");

            var chatRoomMessages = await _chatRoomMessageRepository.GetAllListAsync(chatRoomMessage => chatRoomMessage.ChatRoomId == input.ChatRoomId);

            return new GetChatRoomMessagesOutput
            {
                ChatRoomMessages = chatRoomMessages.MapTo<List<ChatRoomMessageDto>>()
            };
        }

        public async Task<EntityDto<long>> CreateTextChatRoomMessage(CreateTextChatRoomMessageInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var chatRoom = await _chatRoomManager.GetAsync(input.ChatRoomId);

            if (!chatRoom.Participants.Contains(currentUser))
                throw new UserFriendlyException("You are not allowed to create message in this chat room.");

            var chatRoomMessage = await _chatRoomMessageManager.CreateAsync(new TextChatRoomMessage
            {
                Text = input.Text,
                ChatRoom = chatRoom,
                ChatRoomId = chatRoom.Id,
                Owner = currentUser,
                OwnerId = currentUser.Id
            });

            return new EntityDto<long>(chatRoomMessage.Id);
        }
    }
}