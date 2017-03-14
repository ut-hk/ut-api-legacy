using System;
using UniTime.Ratings.Enums;

namespace UniTime.Ratings.Dtos
{
    public class CreateRatingInput
    {
        public RatingStatus RatingStatus { get; set; }

        public Guid? ActivityId { get; set; }

        public Guid? ActivityPlanId { get; set; }

        public Guid? ActivityTemplateId { get; set; }
    }
}