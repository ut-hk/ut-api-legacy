using Abp.UI;
using UniTime.Invitations.Enums;
using UniTime.Users;

namespace UniTime.Invitations
{
    public class FriendInvitation : Invitation
    {
        protected FriendInvitation()
        {
        }

        public static FriendInvitation Create(User invitee, User owner, string content)
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

        internal void Accept(long editUserId)
        {
            

            Status = InvitationStatus.Accepted;
        }

        internal void Reject(long editUserId)
        {
            if (editUserId != InviteeId)
                throw new UserFriendlyException($"You are not allowed to reject this invitation with id = {Id}.");

            if (Status != InvitationStatus.Pending)
                throw new UserFriendlyException($"You are not allowed to change the responded invitation with id = {Id}.");

            Status = InvitationStatus.Rejected;
        }

        internal void Ignore(long editUserId)
        {
            if (editUserId != InviteeId)
                throw new UserFriendlyException($"You are not allowed to ignore this invitation with id = {Id}.");

            if (Status != InvitationStatus.Pending)
                throw new UserFriendlyException($"You are not allowed to change the responded invitation with id = {Id}.");

            Status = InvitationStatus.Ignored;
        }
    }
}