using System.Collections.Generic;

namespace UniTime.Comments.Dtos
{
    public class GetCommentsOutput
    {
        public IReadOnlyList<CommentDto> Comments { get; set; }
    }
}