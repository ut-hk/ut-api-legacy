using System;

namespace UniTime.ChatRooms.Dtos
{
    public class CreateTextChatRoomMessageInput
    {
        public Guid ChatRoomId { get; set; }

        public string Text { get; set; }
    }
}