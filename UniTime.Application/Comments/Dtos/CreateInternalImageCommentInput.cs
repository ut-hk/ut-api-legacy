using System;

namespace UniTime.Comments.Dtos
{
    public class CreateInternalImageCommentInput : CreateCommentInput
    {
        public Guid ImageId { get; set; }
    }
}