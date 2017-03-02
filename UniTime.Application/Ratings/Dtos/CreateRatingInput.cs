using System;
using UniTime.Ratings.Enums;

namespace UniTime.Ratings.Dtos
{
    public class CreateRatingInput
    {
        public RatingStatus RatingStatus { get; set; }

        public Guid? AbstractActivityId { get; set; }

        public Guid? ActivityPlanId { get; set; }
    }
}