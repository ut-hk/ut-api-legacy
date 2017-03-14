using System;

namespace UniTime.Comments.Dtos
{
    public class CreateReplyInput
    {
        public Guid CommentId { get; set; }

        public string Content { get; set; }
    }
}