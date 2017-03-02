using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;

namespace UniTime.Ratings
{
    public class AbstractActivityRating : Rating
    {
        [ForeignKey(nameof(AbstractActivityId))]
        public virtual AbstractActivity AbstractActivity { get; set; }

        public virtual Guid AbstractActivityId { get; set; }
    }
}