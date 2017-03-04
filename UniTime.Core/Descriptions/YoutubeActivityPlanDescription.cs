using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class YoutubeActivityPlanDescription : ActivityPlanDescription
    {
        public override DescriptionType Type => DescriptionType.Youtube;

        public override string Content => YoutubeId;

        public virtual string YoutubeId { get; set; }
    }
}