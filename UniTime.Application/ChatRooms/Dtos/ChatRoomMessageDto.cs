using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using UniTime.ChatRooms.Enums;
using UniTime.Users.Dtos;

namespace UniTime.ChatRooms.Dtos
{
    [AutoMapFrom(typeof(ChatRoomMessage))]
    public class ChatRoomMessageDto : EntityDto<long>, IHasCreationTime
    {
        public string Message { get; set; }

        public ChatRoomMessageType Type { get; set; }

        public long OwnerId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}