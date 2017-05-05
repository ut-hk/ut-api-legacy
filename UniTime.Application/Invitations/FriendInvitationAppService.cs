using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper.QueryableExtensions;
using UniTime.Invitations.Dtos;
using UniTime.Invitations.Enums;
using UniTime.Invitations.Managers;
using UniTime.Invitations.Policies;
using UniTime.Users;
using UniTime.Users.Dtos;

namespace UniTime.Invitations
{
    [AbpAuthorize]
    public class FriendInvitationAppService : UniTimeAppServiceBase, IFriendInvitationAppService
    {
        private readonly IFriendInvitationManager _friendInvitationManager;
        private readonly IFriendInvitationPolicy _friendInvitationPolicy;
        private readonly IRepository<Invitation, Guid> _invitationRepository;
        private readonly IRepository<FriendPair, long> _friendPairRepository;

        public FriendInvitationAppService(
            IRepository<Invitation, Guid> invitationRepository,
            IRepository<FriendPair, long> friendPairRepository,
            IFriendInvitationManager friendInvitationManager,
            IFriendInvitationPolicy friendInvitationPolicy)
        {
            _invitationRepository = invitationRepository;
            _friendPairRepository = friendPairRepository;
            _friendInvitationManager = friendInvitationManager;
            _friendInvitationPolicy = friendInvitationPolicy;
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

            var invitation = await _friendInvitationManager.CreateAsync(FriendInvitation.Create(invitee, currentUser, input.Content, _friendInvitationPolicy));

            return new EntityDto<Guid>(invitation.Id);
        }

        public async Task AcceptFriendInvitation(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var invitation = await _friendInvitationManager.GetAsync(input.Id);

            await _friendInvitationManager.Accept(invitation, currentUserId);
        }

        public async Task RejectFriendInvitation(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var invitation = await _friendInvitationManager.GetAsync(input.Id);

            _friendInvitationManager.Reject(invitation, currentUserId);
        }

        public async Task IgnoreFriendInvitation(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var invitation = await _friendInvitationManager.GetAsync(input.Id);

            _friendInvitationManager.Ignore(invitation, currentUserId);
        }
    }
}