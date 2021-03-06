using System.Collections.Generic;
using System.Data.Entity;
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
            var currentUserId = GetCurrentUserId();
            var chatRoom = await _chatRoomManager.GetAsync(input.ChatRoomId);

            if (!chatRoom.Participants.Select(participant => participant.Id).Contains(currentUserId))
                throw new UserFriendlyException($"You are not allowed to view this chat room with id = {input.ChatRoomId}.");

            var chatRoomMessages = await _chatRoomMessageRepository.GetAll()
                .Where(chatRoomMessage => chatRoomMessage.ChatRoomId == input.ChatRoomId && chatRoomMessage.Id > input.StartId)
                .ToListAsync();

            return new GetChatRoomMessagesOutput
            {
                ChatRoomMessages = chatRoomMessages.MapTo<List<ChatRoomMessageDto>>()
            };
        }

        public async Task<EntityDto<long>> CreateTextChatRoomMessage(CreateTextChatRoomMessageInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var chatRoom = await _chatRoomManager.GetAsync(input.ChatRoomId);

            if (!chatRoom.Participants.Select(participant => participant.Id).Contains(currentUser.Id))
                throw new UserFriendlyException($"You are not allowed to create message in this chat room with id = {input.ChatRoomId}.");

            var chatRoomMessage = await _chatRoomMessageManager.CreateAsync(TextChatRoomMessage.Create(input.Text, chatRoom, currentUser));

            return new EntityDto<long>(chatRoomMessage.Id);
        }
    }
}