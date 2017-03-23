using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Activities.Managers;
using UniTime.Invitations.Dtos;
using UniTime.Invitations.Enums;
using UniTime.Invitations.Managers;
using UniTime.Invitations.Policies;

namespace UniTime.Invitations
{
    [AbpAuthorize]
    public class ActivityInvitationAppService : UniTimeAppServiceBase, IActivityInvitationAppService
    {
        private readonly IActivityInvitationManager _activityInvitationManager;
        private readonly IActivityInvitationPolicy _activityInvitationPolicy;
        private readonly IActivityManager _activityManager;
        private readonly IRepository<Invitation, Guid> _invitationRepository;

        public ActivityInvitationAppService(
            IRepository<Invitation, Guid> invitationRepository,
            IActivityManager activityManager,
            IActivityInvitationManager activityInvitationManager,
            IActivityInvitationPolicy activityInvitationPolicy)
        {
            _invitationRepository = invitationRepository;
            _activityManager = activityManager;
            _activityInvitationManager = activityInvitationManager;
            _activityInvitationPolicy = activityInvitationPolicy;
        }

        public async Task<GetActivityInvitationsOutput> GetMyPendingActivityInvitations()
        {
            var currentUserId = GetCurrentUserId();
            var activityInvitations = await _invitationRepository.GetAll()
                .OfType<ActivityInvitation>()
                .Include(invitation => invitation.Invitee)
                .Include(invitation => invitation.Owner)
                .Include(invitation => invitation.Activity)
                .Where(invitation => invitation.InviteeId == currentUserId && invitation.Status == InvitationStatus.Pending)
                .ToListAsync();

            return new GetActivityInvitationsOutput
            {
                ActivityInvitations = activityInvitations.MapTo<List<ActivityInvitationDto>>()
            };
        }

        public async Task<EntityDto<Guid>> CreateActivityInvitation(CreateActivityInvitationInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var invitee = await UserManager.GetUserByIdAsync(input.InviteeId);
            var activity = await _activityManager.GetAsync(input.ActivityId);

            var invitation = await _activityInvitationManager.CreateAsync(ActivityInvitation.Create(invitee, currentUser, activity, input.Content, _activityInvitationPolicy));

            return new EntityDto<Guid>(invitation.Id);
        }

        public async Task AcceptActivityInvitation(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var invitation = await _activityInvitationManager.GetAsync(input.Id);

            await _activityInvitationManager.Accept(invitation, currentUserId);
        }

        public async Task RejectActivityInvitation(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var invitation = await _activityInvitationManager.GetAsync(input.Id);

            _activityInvitationManager.Reject(invitation, currentUserId);
        }

        public async Task IgnoreActivityInvitation(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var invitation = await _activityInvitationManager.GetAsync(input.Id);

            _activityInvitationManager.Ignore(invitation, currentUserId);
        }
    }
}