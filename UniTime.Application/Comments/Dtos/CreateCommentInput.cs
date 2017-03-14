using System;

namespace UniTime.Comments.Dtos
{
    public class CreateCommentInput
    {
        public Guid? ActivityId { get; set; }

        public Guid? ActivityTemplateId { get; set; }

        public Guid? ActivityPlanId { get; set; }
    }
}