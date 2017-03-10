using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Invitations.Dtos;

namespace UniTime.Invitations
{
    public interface IActivityInvitationAppService : IApplicationService
    {
        Task<GetActivityInvitationsOutput> GetMyActivityInvitations();

        Task<EntityDto<Guid>> CreateActivityInvitation(CreateActivityInvitationInput input);
    }
}