using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;
using UniTime.Users;

namespace UniTime.Comments
{
    public class AbstractActivityComment : Comment
    {
        protected AbstractActivityComment()
        {
        }

        [ForeignKey(nameof(AbstractActivityId))]
        public virtual AbstractActivity AbstractActivity { get; protected set; }

        public virtual Guid AbstractActivityId { get; protected set; }

        public static AbstractActivityComment Create(string content, AbstractActivity abstractActivity, User owner)
        {
            return new AbstractActivityComment
            {
                Content = content,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}