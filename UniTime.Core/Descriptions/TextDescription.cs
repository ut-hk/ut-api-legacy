using Abp.UI;
using UniTime.Activities;
using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class TextDescription : Description
    {
        protected TextDescription()
        {
        }

        public override DescriptionType Type => DescriptionType.Text;

        public override string Content => Text;

        public virtual string Text { get; protected set; }

        public static TextDescription Create(string text, ActivityPlan activityPlan, long createUserId)
        {
            if (createUserId != activityPlan.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a text description in this activity plan with id = {activityPlan.Id}.");

            return new TextDescription
            {
                Text = text,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id
            };
        }

        public static TextDescription Create(string text, AbstractActivity abstractActivity, long createUserId)
        {
            if (createUserId != abstractActivity.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a text description in this activity with id = {abstractActivity.Id}.");

            return new TextDescription
            {
                Text = text,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id
            };
        }

        public void EditText(string text, long editUserId)
        {
            if (editUserId != ActivityPlan.OwnerId)
                throw new UserFriendlyException($"You are not allowed to update this description with id = {Id}.");

            Text = text;
        }
    }
}