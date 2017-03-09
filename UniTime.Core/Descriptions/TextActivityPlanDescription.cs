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

        public virtual string Text { get; set; }

        public static TextActivityPlanDescription Create(ActivityPlan activityPlan)
        {
            return new TextActivityPlanDescription
            {
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id
            };
        }
    }
}