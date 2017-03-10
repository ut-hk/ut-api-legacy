using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Activities.Managers;
using UniTime.Invitations.Dtos;
using UniTime.Invitations.Enums;
using UniTime.Invitations.Managers;

namespace UniTime.Invitations
{
    public class ActivityInvitationAppService : UniTimeAppServiceBase, IActivityInvitationAppService
    {
        private readonly IActivityManager _activityManager;
        private readonly IInvitationManager _invitationManager;
        private readonly IRepository<Invitation, Guid> _invitationRepository;

        public ActivityInvitationAppService(
            IRepository<Invitation, Guid> invitationRepository,
            IActivityManager activityManager,
            IInvitationManager invitationManager)
        {
            _invitationRepository = invitationRepository;
            _activityManager = activityManager;
            _invitationManager = invitationManager;
        }

        public async Task<GetActivityInvitationsOutput> GetMyActivityInvitations()
        {
            var currentUserId = GetCurrentUserId();
            var activityInvitations = await _invitationRepository.GetAll().OfType<ActivityInvitation>()
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

            var invitation = await _invitationManager.CreateAsync(ActivityInvitation.Create(invitee, currentUser, activity, input.Content));

            return new EntityDto<Guid>(invitation.Id);
        }
    }
}