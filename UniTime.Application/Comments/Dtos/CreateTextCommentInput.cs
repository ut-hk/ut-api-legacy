using System.ComponentModel.DataAnnotations;

namespace UniTime.Comments.Dtos
{
    public class CreateTextCommentInput : CreateCommentInput
    {
        [StringLength(TextComment.MaxTextLength, MinimumLength = TextComment.MinTextLength)]
        public string Content { get; set; }
    }
}