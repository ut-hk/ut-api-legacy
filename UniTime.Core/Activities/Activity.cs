using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Invitations;

namespace UniTime.Activities
{
    public class Activity : AbstractActivity
    {
        public virtual DateTime? StarTime { get; set; }

        public virtual DateTime? EndTime { get; set; }

        public virtual ICollection<ActivityParticipant> Participants { get; set; }

        public virtual ICollection<ActivityInvitation> Invitations { get; set; }

        [ForeignKey(nameof(ActivityTemplateId))]
        public virtual ActivityTemplate ActivityTemplate { get; set; }

        public virtual Guid ActivityTemplateId { get; set; }

        public virtual ActivityTemplate ConvertToActivityTemplate()
        {
            return new ActivityTemplate();
        }
    }
}