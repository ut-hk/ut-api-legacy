using UniTime.Activities;
using UniTime.Comments.Enums;
using UniTime.Users;

namespace UniTime.Comments
{
    public class TextComment : Comment
    {
        public override string Content => Text;

        public override CommentType Type => CommentType.Text;

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