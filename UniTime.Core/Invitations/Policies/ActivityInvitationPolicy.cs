using System;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities;
using UniTime.Invitations.Enums;
using UniTime.Users;

namespace UniTime.Invitations.Policies
{
    public class ActivityInvitationPolicy : IActivityInvitationPolicy
    {
        private readonly IRepository<ActivityParticipant, long> _activityParticipantRepository;
        private readonly IRepository<Invitation, Guid> _invitationRepository;

        public ActivityInvitationPolicy(
            IRepository<Invitation, Guid> invitationRepository,
            IRepository<ActivityParticipant, long> activityParticipantRepository)
        {
            _activityParticipantRepository = activityParticipantRepository;
            _invitationRepository = invitationRepository;
        }

        public void CreateAttempt(User invitee, Activity activity, User owner)
        {
            CheckIsOwner(activity, owner, "Only owner can invite people.");
            CheckNoPendingInvitation(invitee, activity);
            CheckIsNotParticipant(activity.Id, owner.Id);
        }

        public void AcceptAttempt(ActivityInvitation activityInvitation, long editUserId)
        {
            CheckIsPending(activityInvitation, editUserId);
            CheckIsInvitee(activityInvitation, editUserId);
            CheckIsNotParticipant(activityInvitation.ActivityId, editUserId);
        }

        public void RejectAttempt(ActivityInvitation activityInvitation, long editUserId)
        {
            CheckIsPending(activityInvitation, editUserId);
            CheckIsInvitee(activityInvitation, editUserId);
        }

        public void IgnoreAttempt(ActivityInvitation activityInvitation, long editUserId)
        {
            CheckIsPending(activityInvitation, editUserId);
            CheckIsInvitee(activityInvitation, editUserId);
        }

        private void CheckNoPendingInvitation(User invitee, Activity activity)
        {
            if (_invitationRepository.GetAll()
                .OfType<ActivityInvitation>()
                .Any(activityInvitation =>
                    activityInvitation.ActivityId == activity.Id &&
                    activityInvitation.InviteeId == invitee.Id &&
                    activityInvitation.Status == InvitationStatus.Pending))
                throw new UserFriendlyException($"You have invited him.");
        }

        private static void CheckIsOwner(Activity activity, User owner, string message)
        {
            if (activity.OwnerId != owner.Id)
                throw new UserFriendlyException(message);
        }

        private static void CheckIsPending(ActivityInvitation activityInvitation, long userId)
        {
            if (activityInvitation.Status != InvitationStatus.Pending)
                throw new UserFriendlyException($"You are not allowed to change the responded invitation with id = {activityInvitation.Id}.");
        }

        private static void CheckIsInvitee(ActivityInvitation activityInvitation, long userId)
        {
            if (userId != activityInvitation.InviteeId)
                throw new UserFriendlyException($"You are not allowed to change this invitation with id = {activityInvitation.Id}.");
        }

        private void CheckIsNotParticipant(Guid activityId, long userId)
        {
            if (_activityParticipantRepository.GetAll().Any(participant => participant.OwnerId == userId && participant.ActivityId == activityId))
                throw new UserFriendlyException("You are one of the participants.");
        }
    }
}