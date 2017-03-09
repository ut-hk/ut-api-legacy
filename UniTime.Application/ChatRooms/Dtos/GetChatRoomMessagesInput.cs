using System;

namespace UniTime.ChatRooms.Dtos
{
    public class GetChatRoomMessagesInput
    {
        public Guid ChatRoomId { get; set; }

        public int StartId { get; set; }
    }
}