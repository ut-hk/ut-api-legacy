using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Users.Dtos;

namespace UniTime.ChatRooms.Dtos
{
    [AutoMapFrom(typeof(ChatRoom))]
    public class ChatRoomDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public IReadOnlyList<UserDto> Participants { get; set; }
    }
}