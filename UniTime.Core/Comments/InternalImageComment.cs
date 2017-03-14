using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;
using UniTime.Comments.Enums;
using UniTime.Files;
using UniTime.Users;

namespace UniTime.Comments
{
    public class InternalImageComment : Comment
    {
        public override string Content => ImageId.ToString();

        public override CommentType Type => CommentType.InternalImage;

        [ForeignKey(nameof(ImageId))]
        public virtual Image Image { get; protected set; }

        public virtual Guid ImageId { get; protected set; }

        public static InternalImageComment Create(Image image, AbstractActivity abstractActivity, User owner)
        {
            return new InternalImageComment
            {
                Image = image,
                ImageId = image.Id,
                AbstractActivity = abstractActivity,
                AbstractActivityId = abstractActivity.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }

        public static InternalImageComment Create(Image image, ActivityPlan activityPlan, User owner)
        {
            return new InternalImageComment
            {
                Image = image,
                ImageId = image.Id,
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}