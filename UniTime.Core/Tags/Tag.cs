using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using UniTime.Activities;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniTime.Tags
{
    public class Tag : FullAuditedEntity<long>
    {
        public const int MaxTextLength = 256;
        public const int MinTextLength = 1;

        protected Tag()
        {
        }

        [Required]
        [Index]
        [StringLength(MaxTextLength, MinimumLength = MinTextLength)]
        public virtual string Text { get; protected set; }

        public virtual ICollection<AbstractActivity> Activities { get; protected set; }

        public virtual ICollection<ActivityPlan> ActivityPlans { get; protected set; }

        public static Tag Create(string text)
        {
            if (text.Contains(" "))
                throw new UserFriendlyException("A tag must not contain a space.");

            return new Tag
            {
                Text = text
            };
        }
    }
}