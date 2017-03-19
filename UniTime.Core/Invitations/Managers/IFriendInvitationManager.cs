using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Invitations.Managers
{
    public interface IFriendInvitationManager : IDomainService
    {
        Task<FriendInvitation> GetAsync(Guid id);

        Task<FriendInvitation> CreateAsync(FriendInvitation friendInvitation);

        Task Accept(FriendInvitation friendInvitation, long editUserId);
        void Reject(FriendInvitation friendInvitation, long editUserId);
        void Ignore(FriendInvitation friendInvitation, long editUserId);
    }
}