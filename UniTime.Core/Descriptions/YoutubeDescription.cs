using UniTime.Activities;
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

        public static YoutubeDescription Create(string youtubeId, ActivityPlan activityPlan, long createUserId)
        {
            return new YoutubeDescription()
            {
                YoutubeId = youtubeId,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id,
            };
        }
    }
}