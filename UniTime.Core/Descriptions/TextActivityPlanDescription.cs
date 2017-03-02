using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class TextActivityPlanDescription : ActivityPlanDescription
    {
        public override string Content => Text;

        public virtual string Text { get; set; }

        public override DescriptionType Type => DescriptionType.Text;
    }
}