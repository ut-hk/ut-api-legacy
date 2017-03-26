using System.ComponentModel.DataAnnotations;
using UniTime.Activities;
using UniTime.Comments.Enums;
using UniTime.Users;

namespace UniTime.Comments
{
    public class TextComment : Comment
    {
        public const int MaxTextLength = 512;
        public const int MinTextLength = 1;

        public override string Content => Text;

        public override CommentType Type => CommentType.Text;

        [Required]
        [StringLength(MaxTextLength, MinimumLength = MinTextLength)]
        public virtual string Text { get; protected set; }

        public static TextComment Create(string content, AbstractActivity abstractActivity, User owner)
        {
            return new TextComment
            {
                Text = content,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }

        public static TextComment Create(string content, ActivityPlan activityPlan, User owner)
        {
            return new TextComment
            {
                Text = content,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}