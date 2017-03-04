using System;

namespace UniTime.Comments.Dtos
{
    public class CreateCommentInput
    {
        public string Content { get; set; }

        public Guid? AbstractActivityId { get; set; }

        public Guid? ActivityPlanId { get; set; }
    }
}