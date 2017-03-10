using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.ChatRooms.Dtos;
using UniTime.ChatRooms.Managers;
using UniTime.Users;

namespace UniTime.ChatRooms
{
    [AbpAuthorize]
    public class ChatRoomAppService : UniTimeAppServiceBase, IChatRoomAppService
    {
        private readonly IChatRoomManager _chatRoomManager;
        private readonly IRepository<ChatRoom, Guid> _chatRoomRepository;
        private readonly IRepository<User, long> _userRepository;

        public ChatRoomAppService(
            IRepository<ChatRoom, Guid> chatRoomRepository,
            IRepository<User, long> userRepository,
            IChatRoomManager chatRoomManager)
        {
            _chatRoomRepository = chatRoomRepository;
            _userRepository = userRepository;
            _chatRoomManager = chatRoomManager;
        }

        public async Task<GetMyChatRoomsOutput> GetMyChatRooms()
        {
            var currentUser = await GetCurrentUserAsync();

            var chatRooms = await _chatRoomRepository.GetAllListAsync(chatRoom =>
                chatRoom.Participants.Select(participant => participant.Id).Contains(currentUser.Id));

            return new GetMyChatRoomsOutput
            {
                ChatRooms = chatRooms.MapTo<List<ChatRoomDto>>()
            };
        }

        public async Task<EntityDto<Guid>> CreateChatRoom(CreateChatRoomInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var chatRoom = await _chatRoomManager.CreateAsync(ChatRoom.Create(input.Name, currentUser, new List<User>()));

            return new EntityDto<Guid>(chatRoom.Id);
        }

        public async Task UpdateChatRoom(UpdateChatRoomInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var chatRoom = await _chatRoomManager.GetAsync(input.Id);
            var participants = await _userRepository.GetAllListAsync(user => input.ParticipantIds.Contains(user.Id));

            chatRoom.EditName(input.Name, currentUser);
            chatRoom.EditParticipants(participants, currentUser);
        }
    }
}