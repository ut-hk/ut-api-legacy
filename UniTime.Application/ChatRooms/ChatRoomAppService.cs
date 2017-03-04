using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.ChatRooms.Dtos;
using UniTime.ChatRooms.Managers;
using UniTime.Users;

namespace UniTime.ChatRooms
{
    public class ChatRoomAppService : UniTimeAppServiceBase, IChatRoomAppService
    {
        private readonly IChatRoomManager _chatRoomManager;
        private readonly IRepository<ChatRoom, Guid> _chatRoomRepository;

        public ChatRoomAppService(
            IRepository<ChatRoom, Guid> chatRoomRepository,
            IChatRoomManager chatRoomManager)
        {
            _chatRoomRepository = chatRoomRepository;
            _chatRoomManager = chatRoomManager;
        }

        /// <summary>
        ///     My Chat Rooms are the participated chat rooms.
        /// </summary>
        /// <returns></returns>
        public async Task<GetMyChatRoomsOutput> GetMyChatRooms()
        {
            var currentUser = await GetCurrentUserAsync();

            var chatRooms = await _chatRoomRepository.GetAllListAsync(chatRoom => chatRoom.Participants.Select(participant => participant.Id).Contains(currentUser.Id));

            return new GetMyChatRoomsOutput
            {
                ChatRooms = chatRooms.MapTo<List<ChatRoomDto>>()
            };
        }

        public async Task<EntityDto<Guid>> CreateChatRoom(CreateChatRoomInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var chatRoom = await _chatRoomManager.CreateAsync(new ChatRoom
            {
                Name = input.Name,
                Participants = new List<User>
                {
                    currentUser
                }
            });

            return new EntityDto<Guid>(chatRoom.Id);
        }
    }
}