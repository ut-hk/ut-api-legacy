using UniTime.Invitations.Enums;
using UniTime.Users;

namespace UniTime.Invitations
{
    public class FriendInvitation : Invitation
    {
        protected FriendInvitation()
        {
        }

        public static Invitation Create(User invitee, User owner, string content)
        {
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
    }
}