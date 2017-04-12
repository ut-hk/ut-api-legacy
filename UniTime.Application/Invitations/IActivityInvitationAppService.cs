using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Invitations.Dtos;

namespace UniTime.Invitations
{
    public interface IActivityInvitationAppService : IApplicationService
    {
        Task<GetActivityInvitationsOutput> GetMyPendingActivityInvitations();

        Task<CreateActivityInvitationsOutput> CreateActivityInvitations(CreateActivityInvitationsInput input);

        Task AcceptActivityInvitation(EntityDto<Guid> input);
        Task RejectActivityInvitation(EntityDto<Guid> input);
        Task IgnoreActivityInvitation(EntityDto<Guid> input);
    }
}