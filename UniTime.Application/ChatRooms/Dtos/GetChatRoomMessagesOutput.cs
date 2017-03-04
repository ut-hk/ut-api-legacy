using System.Collections.Generic;
using UniTime.ChatRooms.Dtos;

namespace UniTime.ChatRooms
{
    public class GetChatRoomMessagesOutput
    {
        public IReadOnlyList<ChatRoomMessageDto> ChatRoomMessages { get; set; }
    }
}