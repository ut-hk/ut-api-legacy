using System;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Invitations.Enums;
using UniTime.Users;

namespace UniTime.Invitations.Policies
{
    public class FriendInvitationPolicy : IFriendInvitationPolicy
    {
        private readonly IRepository<FriendPair, long> _friendPairRepository;
        private readonly IRepository<Invitation, Guid> _invitationRepository;

        public FriendInvitationPolicy(
            IRepository<Invitation, Guid> invitationRepository,
            IRepository<FriendPair, long> friendPairRepository)
        {
            _friendPairRepository = friendPairRepository;
            _invitationRepository = invitationRepository;
        }
        
        public void CreateAttempt(User invitee, User owner)
        {
            CheckNoPendingInvitation(invitee, owner);
            CheckIsNotFriend(invitee, owner);
        }

        public void AcceptAttempt(FriendInvitation friendInvitation, long editUserId)
        {
            CheckIsInvitee(friendInvitation, editUserId);
            CheckIsPending(friendInvitation, editUserId);
        }

        public void RejectAttempt(FriendInvitation friendInvitation, long editUserId)
        {
            CheckIsInvitee(friendInvitation, editUserId);
            CheckIsPending(friendInvitation, editUserId);
        }

        public void IgnoreAttempt(FriendInvitation friendInvitation, long editUserId)
        {
            CheckIsInvitee(friendInvitation, editUserId);
            CheckIsPending(friendInvitation, editUserId);
        }

        private void CheckIsNotFriend(User invitee, User owner)
        {
            if (_friendPairRepository.GetAll()
                .Any(fp =>
                    fp.LeftId == owner.Id || fp.LeftId == invitee.Id &&
                    fp.RightId == owner.Id || fp.RightId == invitee.Id))
                throw new UserFriendlyException($"You two are friend already.");
        }

        private void CheckNoPendingInvitation(User invitee, User owner)
        {
            if (_invitationRepository.GetAll()
                .OfType<FriendInvitation>()
                .Any(fi =>
                    fi.OwnerId == owner.Id && fi.InviteeId == invitee.Id &&
                    fi.Status == InvitationStatus.Pending))
                throw new UserFriendlyException($"You have invited him.");
        }

        private static void CheckIsPending(FriendInvitation friendInvitation, long userId)
        {
            if (friendInvitation.Status != InvitationStatus.Pending)
                throw new UserFriendlyException($"You are not allowed to change the responded invitation with id = {friendInvitation.Id}.");
        }

        private static void CheckIsInvitee(FriendInvitation friendInvitation, long userId)
        {
            if (userId != friendInvitation.InviteeId)
                throw new UserFriendlyException($"You are not allowed to change this invitation with id = {friendInvitation.Id}.");
        }
    }
}