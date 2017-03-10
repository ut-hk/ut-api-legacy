using Abp.UI;
using UniTime.Activities;
using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class TextActivityPlanDescription : ActivityPlanDescription
    {
        protected TextActivityPlanDescription()
        {
        }

        public override DescriptionType Type => DescriptionType.Text;

        public override string Content => Text;

        public virtual string Text { get; protected set; }

        public static TextActivityPlanDescription Create(ActivityPlan activityPlan, long createUserId)
        {
            if (createUserId != activityPlan.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a text description in this description with id = {activityPlan.Id}.");


            return new TextActivityPlanDescription
            {
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id
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