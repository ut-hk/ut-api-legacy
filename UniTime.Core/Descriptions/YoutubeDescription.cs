using Abp.UI;
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
            if (createUserId != activityPlan.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a text description in this activity plan with id = {activityPlan.Id}.");

            return new YoutubeDescription
            {
                YoutubeId = youtubeId,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id
            };
        }

        public static YoutubeDescription Create(string youtubeId, AbstractActivity abstractActivity, long createUserId)
        {
            if (createUserId != abstractActivity.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a text description in this activity with id = {abstractActivity.Id}.");

            return new YoutubeDescription
            {
                YoutubeId = youtubeId,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id
            };
        }
    }
}