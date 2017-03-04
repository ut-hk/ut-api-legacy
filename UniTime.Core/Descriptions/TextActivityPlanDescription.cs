using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class TextActivityPlanDescription : ActivityPlanDescription
    {
        public override DescriptionType Type => DescriptionType.Text;

        public override string Content => Text;

        public virtual string Text { get; set; }
    }
}