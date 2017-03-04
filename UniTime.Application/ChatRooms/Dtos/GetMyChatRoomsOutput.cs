using System.Collections.Generic;

namespace UniTime.ChatRooms.Dtos
{
    public class GetMyChatRoomsOutput
    {
        public IReadOnlyList<ChatRoomDto> ChatRooms { get; set; }
    }
}