using UniTime.ChatRooms.Enums;
using UniTime.Users;

namespace UniTime.ChatRooms
{
    public class TextChatRoomMessage : ChatRoomMessage
    {
        protected TextChatRoomMessage()
        {
        }

        public override ChatRoomMessageType Type => ChatRoomMessageType.Text;

        public override string Message => Text;

        public virtual string Text { get; protected set; }

        public static TextChatRoomMessage Create(string text, ChatRoom chatRoom, User owner)
        {
            return new TextChatRoomMessage
            {
                Text = text,
                ChatRoom = chatRoom,
                ChatRoomId = chatRoom.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}