using Abp.Domain.Services;
using UniTime.Users;

namespace UniTime.Invitations.Policies
{
    public interface IFriendInvitationPolicy : IDomainService
    {
        void CreateAttempt(User invitee, User owner);

        void AcceptAttempt(FriendInvitation friendInvitation, long editUserId);
        void RejectAttempt(FriendInvitation friendInvitation, long editUserId);
        void IgnoreAttempt(FriendInvitation friendInvitation, long editUserId);
    }
}