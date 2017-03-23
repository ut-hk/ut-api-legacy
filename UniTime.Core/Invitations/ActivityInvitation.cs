using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniTime.Activities;
using UniTime.Invitations.Enums;
using UniTime.Invitations.Policies;
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

        public static ActivityInvitation Create(User invitee, User owner, Activity activity, string content, IActivityInvitationPolicy activityInvitationPolicy)
        {
            activityInvitationPolicy.CreateAttempt(invitee, activity,owner);

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

        internal void Accept(long editUserId, IActivityInvitationPolicy activityInvitationPolicy)
        {
            activityInvitationPolicy.AcceptAttempt(this, editUserId);

            Status = InvitationStatus.Accepted;
        }

        internal void Reject(long editUserId, IActivityInvitationPolicy activityInvitationPolicy)
        {
            activityInvitationPolicy.RejectAttempt(this, editUserId);
           
            Status = InvitationStatus.Rejected;
        }

        internal void Ignore(long editUserId, IActivityInvitationPolicy activityInvitationPolicy)
        {
            activityInvitationPolicy.IgnoreAttempt(this, editUserId);
            
            Status = InvitationStatus.Ignored;
        }
    }
}