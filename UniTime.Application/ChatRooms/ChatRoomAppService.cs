using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper.QueryableExtensions;
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
            var currentUserId =  GetCurrentUserId();

            var chatRooms = await _chatRoomRepository.GetAll()
                .Where(chatRoom => chatRoom.Participants.Select(participant => participant.Id).Contains(currentUserId))
                .ProjectTo<ChatRoomDto>()
                .ToListAsync();

            return new GetMyChatRoomsOutput
            {
                ChatRooms = chatRooms
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
            var currentUserId = GetCurrentUserId();

            var chatRoom = await _chatRoomManager.GetAsync(input.Id);
            var participants = await _userRepository.GetAllListAsync(user => input.ParticipantIds.Contains(user.Id));

            _chatRoomManager.EditChatRoom(chatRoom, input.Name, currentUserId);
            _chatRoomManager.EditParticipants(chatRoom, participants, currentUserId);
        }
    }
}