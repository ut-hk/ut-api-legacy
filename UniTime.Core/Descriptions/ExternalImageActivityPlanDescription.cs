using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class ExternalImageActivityPlanDescription : ActivityPlanDescription
    {
        protected ExternalImageActivityPlanDescription()
        {
        }

        public override DescriptionType Type => DescriptionType.ExternalImage;

        public override string Content => Path;

        public virtual string Path { get; set; }
    }
}