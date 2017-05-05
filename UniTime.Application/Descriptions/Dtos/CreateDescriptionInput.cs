using System;

namespace UniTime.Descriptions.Dtos
{
    public class CreateDescriptionInput
    {
        public Guid? ActivityId { get; set; }

        public Guid? ActivityTemplateId { get; set; }

        public Guid? ActivityPlanId { get; set; }

        public int? Priority { get; set; }
    }
}