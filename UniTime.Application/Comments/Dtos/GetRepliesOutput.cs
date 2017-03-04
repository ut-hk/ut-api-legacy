using System.Collections.Generic;

namespace UniTime.Comments.Dtos
{
    public class GetRepliesOutput
    {
        public IReadOnlyList<ReplyDto> Replies { get; set; }
    }
}