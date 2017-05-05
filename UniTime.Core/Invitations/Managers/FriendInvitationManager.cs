using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Invitations.Policies;
using UniTime.Users;

namespace UniTime.Invitations.Managers
{
    public class FriendInvitationManager : IFriendInvitationManager
    {
        private readonly IFriendInvitationPolicy _friendInvitationPolicy;
        private readonly IRepository<FriendPair, long> _friendPairRepository;
        private readonly IRepository<Invitation, Guid> _invitationRepository;

        public FriendInvitationManager(
            IRepository<Invitation, Guid> invitationRepository,
            IRepository<FriendPair, long> friendPairRepository,
            IFriendInvitationPolicy friendInvitationPolicy)
        {
            _invitationRepository = invitationRepository;
            _friendPairRepository = friendPairRepository;
            _friendInvitationPolicy = friendInvitationPolicy;
        }

        public async Task<FriendInvitation> GetAsync(Guid id)
        {
            var activityInvitation = await _invitationRepository.FirstOrDefaultAsync(id) as FriendInvitation;

            if (activityInvitation == null)
                throw new UserFriendlyException("The invitation with id = " + id + " does not exist.");

            return activityInvitation;
        }

        public async Task<FriendInvitation> CreateAsync(FriendInvitation friendInvitation)
        {
            friendInvitation.Id = await _invitationRepository.InsertAndGetIdAsync(friendInvitation);

            return friendInvitation;
        }

        public async Task Accept(FriendInvitation friendInvitation, long editUserId)
        {
            friendInvitation.Accept(editUserId, _friendInvitationPolicy);

            await _friendPairRepository.InsertAsync(FriendPair.Create(friendInvitation.Owner, friendInvitation.Invitee));
        }

        public void Reject(FriendInvitation friendInvitation, long editUserId)
        {
            friendInvitation.Reject(editUserId, _friendInvitationPolicy);
        }

        public void Ignore(FriendInvitation friendInvitation, long editUserId)
        {
            friendInvitation.Ignore(editUserId, _friendInvitationPolicy);
        }
    }
}