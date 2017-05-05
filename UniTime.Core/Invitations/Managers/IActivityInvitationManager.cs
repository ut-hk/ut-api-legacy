using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Invitations.Managers
{
    public interface IActivityInvitationManager : IDomainService
    {
        Task<ActivityInvitation> GetAsync(Guid id);

        Task<ActivityInvitation> CreateAsync(ActivityInvitation activityInvitation);

        Task Accept(ActivityInvitation activityInvitation, long editUserId);
        void Reject(ActivityInvitation activityInvitation, long editUserId);
        void Ignore(ActivityInvitation activityInvitation, long editUserId);
    }
}