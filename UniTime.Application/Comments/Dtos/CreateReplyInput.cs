namespace UniTime.Comments.Dtos
{
    public class CreateReplyInput
    {
        public long CommentId { get; set; }

        public string Content { get; set; }
    }
}