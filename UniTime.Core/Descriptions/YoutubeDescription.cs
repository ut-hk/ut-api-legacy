using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class YoutubeDescription : Description
    {
        protected YoutubeDescription()
        {
        }

        public override DescriptionType Type => DescriptionType.Youtube;

        public override string Content => YoutubeId;

        public virtual string YoutubeId { get; protected set; }
    }
}