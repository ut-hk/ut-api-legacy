using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class YoutubeActivityPlanDescription : ActivityPlanDescription
    {
        public override string Content => YoutubeId;

        public virtual string YoutubeId { get; set; }

        public override DescriptionType Type => DescriptionType.Youtube;
    }
}