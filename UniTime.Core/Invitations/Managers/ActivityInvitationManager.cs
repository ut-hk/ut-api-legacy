using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities;
using UniTime.Invitations.Policies;

namespace UniTime.Invitations.Managers
{
    public class ActivityInvitationManager : IActivityInvitationManager
    {
        private readonly IActivityInvitationPolicy _activityInvitationPolicy;
        private readonly IRepository<ActivityParticipant, long> _activityParticipantRepository;
        private readonly IRepository<Invitation, Guid> _invitationRepository;

        public ActivityInvitationManager(
            IRepository<Invitation, Guid> invitationRepository,
            IRepository<ActivityParticipant, long> activityParticipantRepository,
            IActivityInvitationPolicy activityInvitationPolicy)
        {
            _invitationRepository = invitationRepository;
            _activityParticipantRepository = activityParticipantRepository;
            _activityInvitationPolicy = activityInvitationPolicy;
        }

        public async Task<ActivityInvitation> GetAsync(Guid id)
        {
            var activityInvitation = await _invitationRepository.FirstOrDefaultAsync(id) as ActivityInvitation;

            if (activityInvitation == null)
                throw new UserFriendlyException("The invitation with id = " + id + " does not exist.");

            return activityInvitation;
        }

        public async Task<ActivityInvitation> CreateAsync(ActivityInvitation activityInvitation)
        {
            activityInvitation.Id = await _invitationRepository.InsertAndGetIdAsync(activityInvitation);

            return activityInvitation;
        }

        public async Task Accept(ActivityInvitation activityInvitation, long editUserId)
        {
            activityInvitation.Accept(editUserId, _activityInvitationPolicy);

            await _activityParticipantRepository.InsertAsync(ActivityParticipant.Create(activityInvitation.Activity, activityInvitation.Invitee));
        }

        public void Reject(ActivityInvitation activityInvitation, long editUserId)
        {
            activityInvitation.Reject(editUserId, _activityInvitationPolicy);
        }

        public void Ignore(ActivityInvitation activityInvitation, long editUserId)
        {
            activityInvitation.Ignore(editUserId, _activityInvitationPolicy);
        }
    }
}