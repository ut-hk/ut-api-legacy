using UniTime.ChatRooms.Enums;

namespace UniTime.ChatRooms
{
    public class TextChatRoomMessage : ChatRoomMessage
    {
        public override ChatRoomMessageType Type => ChatRoomMessageType.Text;

        public override string Message => Text;

        public virtual string Text { get; set; }
    }
}