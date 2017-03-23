using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.UI;
using UniTime.Activities;
using UniTime.Descriptions.Enums;
using UniTime.Files;

namespace UniTime.Descriptions
{
    public class InternalImageDescription : Description
    {
        protected InternalImageDescription()
        {
        }

        public override DescriptionType Type => DescriptionType.InternalImage;

        public override string Content => ImageId.ToString();

        [ForeignKey(nameof(ImageId))]
        public virtual Image Image { get; protected set; }

        public virtual Guid ImageId { get; protected set; }

        public static InternalImageDescription Create(Image image, ActivityPlan activityPlan, long createUserId)
        {
            if (createUserId != activityPlan.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a internal description in this activity plan with id = {activityPlan.Id}.");

            return new InternalImageDescription
            {
                Image = image,
                ImageId = image.Id,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id
            };
        }

        public static InternalImageDescription Create(Image image, AbstractActivity abstractActivity, long createUserId)
        {
            if (createUserId != abstractActivity.OwnerId)
                throw new UserFriendlyException($"You are not allowed to create a internal description in this activity with id = {abstractActivity.Id}.");

            return new InternalImageDescription
            {
                Image = image,
                ImageId = image.Id,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id
            };
        }
    }
}