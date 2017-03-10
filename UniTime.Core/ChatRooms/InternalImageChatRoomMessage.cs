using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.ChatRooms.Enums;
using UniTime.Files;

namespace UniTime.ChatRooms
{
    public class InternalImageChatRoomMessage : ChatRoomMessage
    {
        protected InternalImageChatRoomMessage()
        {
        }

        public override ChatRoomMessageType Type => ChatRoomMessageType.InternalImage;

        public override string Message => Image.Id.ToString();

        [ForeignKey(nameof(ImageId))]
        public virtual Image Image { get; protected set; }

        public virtual Guid ImageId { get; protected set; }
    }
}