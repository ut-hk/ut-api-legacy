using System;
using Abp.UI;
using UniTime.Activities;
using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions
{
    public class ExternalImageDescription : Description
    {
        protected ExternalImageDescription()
        {
        }

        public override DescriptionType Type => DescriptionType.ExternalImage;

        public override string Content => $"https://images.weserv.nl/?url={Path}&output=jpg";

        public virtual string Path { get; protected set; }

        public static ExternalImageDescription Create(string path, ActivityPlan activityPlan, long createUserId)
        {
            if (createUserId != activityPlan.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a external image description in this activity plan with id = {activityPlan.Id}.");

            var uri = new Uri(path);

            return new ExternalImageDescription
            {
                Path = uri.Host + uri.PathAndQuery,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id
            };
        }

        public static ExternalImageDescription Create(string path, AbstractActivity abstractActivity, long createUserId)
        {
            if (createUserId != abstractActivity.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a external image description in this activity with id = {abstractActivity.Id}.");

            var uri = new Uri(path);

            return new ExternalImageDescription
            {
                Path = uri.Host + uri.PathAndQuery,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id
            };
        }
    }
}