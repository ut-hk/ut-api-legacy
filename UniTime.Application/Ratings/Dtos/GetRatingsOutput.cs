using System.Collections.Generic;

namespace UniTime.Ratings.Dtos
{
    public class GetRatingsOutput
    {
        public IReadOnlyList<RatingDto> Ratings { get; set; }
    }
}