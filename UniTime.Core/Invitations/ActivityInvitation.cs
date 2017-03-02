using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;

namespace UniTime.Invitations
{
    public class ActivityInvitation : Invitation
    {
        [ForeignKey(nameof(ActivityId))]
        public virtual Activity Activity { get; set; }

        public virtual Guid ActivityId { get; set; }
    }
}