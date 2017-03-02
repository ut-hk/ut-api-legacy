using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;

namespace UniTime.Comments
{
    public class AbstractActivityComment : Comment
    {
        [ForeignKey(nameof(AbstractActivityId))]
        public virtual AbstractActivity AbstractActivity { get; set; }

        public virtual Guid AbstractActivityId { get; set; }
    }
}