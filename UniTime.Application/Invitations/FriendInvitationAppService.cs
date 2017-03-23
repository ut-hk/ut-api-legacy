using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Invitations.Dtos;
using UniTime.Invitations.Enums;
using UniTime.Invitations.Managers;

namespace UniTime.Invitations
{
    [AbpAuthorize]
    public class FriendInvitationAppService : UniTimeAppServiceBase, IFriendInvitationAppService
    {
        private readonly IFriendInvitationManager _friendInvitationManager;
        private readonly IRepository<Invitation, Guid> _invitationRepository;

        public FriendInvitationAppService(
            IRepository<Invitation, Guid> invitationRepository,
            IFriendInvitationManager friendInvitationManager)
        {
            _invitationRepository = invitationRepository;
            _friendInvitationManager = friendInvitationManager;
        }

        public async Task<GetFriendInvitationsOutput> GetMyPendingFriendInvitations()
        {
            var currentUserId = GetCurrentUserId();

            var friendInvitations = await _invitationRepository.GetAll()
                .OfType<FriendInvitation>()
                .Include(invitation => invitation.Invitee)
                .Include(invitation => invitation.Owner)
                .Where(invitation => invitation.InviteeId == currentUserId && invitation.Status == InvitationStatus.Pending)
                .ToListAsync();

            return new GetFriendInvitationsOutput
            {
                FriendInvitations = friendInvitations.MapTo<List<FriendInvitationDto>>()
            };
        }

        public async Task<EntityDto<Guid>> CreateFriendInvitation(CreateFriendInvitationInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var invitee = await UserManager.GetUserByIdAsync(input.InviteeId);

            var invitation = await _friendInvitationManager.CreateAsync(FriendInvitation.Create(invitee, currentUser, input.Content));

            return new EntityDto<Guid>(invitation.Id);
        }
    }
}