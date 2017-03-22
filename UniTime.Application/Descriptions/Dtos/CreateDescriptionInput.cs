using System;

namespace UniTime.Descriptions.Dtos
{
    public class CreateDescriptionInput
    {
        public Guid? ActivityPlanId { get; set; }

        public Guid? AbstractActivityId { get; set; }
    }
}