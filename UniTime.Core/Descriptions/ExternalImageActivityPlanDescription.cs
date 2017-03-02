using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class ExternalImageActivityPlanDescription : ActivityPlanDescription
    {
        public override string Content => Path;

        public virtual string Path { get; set; }

        public override DescriptionType Type => DescriptionType.ExternalImage;
    }
}