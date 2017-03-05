using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using UniTime.Activities;

namespace UniTime.Tags
{
    public class Tag : FullAuditedEntity<long>
    {
        public const int MaxTextLength = 256;
        public const int MinTextLength = 2;

        protected Tag()
        {
        }

        [Required]
        [StringLength(MaxTextLength, MinimumLength = MinTextLength)]
        public virtual string Text { get; protected set; }

        public virtual ICollection<AbstractActivity> Activities { get; set; }

        public virtual ICollection<ActivityPlan> ActivityPlans { get; set; }

        public static Tag Create(string text)
        {
            if (text.Contains(" "))
                throw new UserFriendlyException("A tag should not contain a space.");

            return new Tag
            {
                Text = text
            };
        }
    }
}