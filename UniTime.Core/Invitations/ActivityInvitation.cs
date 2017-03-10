using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;
using UniTime.Invitations.Enums;
using UniTime.Users;

namespace UniTime.Invitations
{
    public class ActivityInvitation : Invitation
    {
        protected ActivityInvitation()
        {
        }

        [ForeignKey(nameof(ActivityId))]
        public virtual Activity Activity { get; protected set; }

        public virtual Guid ActivityId { get; protected set; }

        public static ActivityInvitation Create(User invitee, User owner, Activity activity, string content)
        {
            return new ActivityInvitation
            {
                Content = content,
                Status = InvitationStatus.Pending,
                Invitee = invitee,
                InviteeId = invitee.Id,
                Owner = owner,
                OwnerId = owner.Id,
                Activity = activity,
                ActivityId = activity.Id
            };
        }
    }
}