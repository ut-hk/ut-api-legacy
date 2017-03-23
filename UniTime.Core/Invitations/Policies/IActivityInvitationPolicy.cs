using Abp.Domain.Services;
using UniTime.Activities;
using UniTime.Users;

namespace UniTime.Invitations.Policies
{
    public interface IActivityInvitationPolicy : IDomainService
    {
        void CreateAttempt(User invitee, Activity activity, User owner);

        void AcceptAttempt(ActivityInvitation activityInvitation, long editUserId);
        void RejectAttempt(ActivityInvitation activityInvitation, long editUserId);
        void IgnoreAttempt(ActivityInvitation activityInvitation, long editUserId);
    }
}