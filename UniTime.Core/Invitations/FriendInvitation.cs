using UniTime.Invitations.Enums;
using UniTime.Invitations.Policies;
using UniTime.Users;

namespace UniTime.Invitations
{
    public class FriendInvitation : Invitation
    {
        protected FriendInvitation()
        {
        }

        public static FriendInvitation Create(User invitee, User owner, string content, IFriendInvitationPolicy friendInvitationPolicy)
        {
            friendInvitationPolicy.CreateAttempt(invitee, owner);

            return new FriendInvitation
            {
                Content = content,
                Status = InvitationStatus.Pending,
                Invitee = invitee,
                InviteeId = invitee.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }

        internal void Accept(long editUserId, IFriendInvitationPolicy friendInvitationPolicy)
        {
            friendInvitationPolicy.AcceptAttempt(this, editUserId);

            Status = InvitationStatus.Accepted;
        }

        internal void Reject(long editUserId, IFriendInvitationPolicy friendInvitationPolicy)
        {
            friendInvitationPolicy.RejectAttempt(this, editUserId);

            Status = InvitationStatus.Rejected;
        }

        internal void Ignore(long editUserId, IFriendInvitationPolicy friendInvitationPolicy)
        {
            friendInvitationPolicy.IgnoreAttempt(this, editUserId);

            Status = InvitationStatus.Ignored;
        }
    }
}