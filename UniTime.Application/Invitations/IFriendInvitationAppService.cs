using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Invitations.Dtos;

namespace UniTime.Invitations
{
    public interface IFriendInvitationAppService : IApplicationService
    {
        Task<GetFriendInvitationsOutput> GetMyPendingFriendInvitations();

        Task<EntityDto<Guid>> CreateFriendInvitation(CreateFriendInvitationInput input);

        Task AcceptFriendInvitation(EntityDto<Guid> input);
        Task RejectFriendInvitation(EntityDto<Guid> input);
        Task IgnoreFriendInvitation(EntityDto<Guid> input);
    }
}