using System;
using Abp.Application.Services.Dto;

namespace UniTime.ChatRooms.Dtos
{
    public class UpdateChatRoomInput : EntityDto<Guid>
    {
        public string Name { get; set; }

        public long[] ParticipantIds { get; set; }
    }
}