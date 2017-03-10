using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class ExternalImageDescription : Description
    {
        protected ExternalImageDescription()
        {
        }

        public override DescriptionType Type => DescriptionType.ExternalImage;

        public override string Content => Path;

        public virtual string Path { get; protected set; }
    }
}