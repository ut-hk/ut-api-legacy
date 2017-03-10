using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Invitations.Managers
{
    public class InvitationManager : IInvitationManager
    {
        private readonly IRepository<Invitation, Guid> _invitationRepository;

        public InvitationManager(
            IRepository<Invitation, Guid> invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task<Invitation> GetAsync(Guid id)
        {
            var invitation = await _invitationRepository.FirstOrDefaultAsync(id);

            if (invitation == null) throw new UserFriendlyException("The invitation with id = " + id + " does not exist.");

            return invitation;
        }

        public async Task<Invitation> CreateAsync(Invitation invitation)
        {
            invitation.Id = await _invitationRepository.InsertAndGetIdAsync(invitation);

            return invitation;
        }
    }
}