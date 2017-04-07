using System;
using System.ComponentModel.DataAnnotations;

namespace UniTime.Comments.Dtos
{
    public class CreateReplyInput
    {
        public Guid CommentId { get; set; }

        [StringLength(Reply.MaxContentLength, MinimumLength = Reply.MinContentLength)]
        public string Content { get; set; }
    }
}