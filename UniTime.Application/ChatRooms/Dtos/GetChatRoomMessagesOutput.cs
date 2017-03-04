using System.Collections.Generic;

namespace UniTime.ChatRooms.Dtos
{
    public class GetChatRoomMessagesOutput
    {
        public IReadOnlyList<ChatRoomMessageDto> ChatRoomMessages { get; set; }
    }
}