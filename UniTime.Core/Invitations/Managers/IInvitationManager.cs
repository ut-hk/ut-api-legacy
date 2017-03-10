using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Invitations.Managers
{
    public interface IInvitationManager : IDomainService
    {
        Task<Invitation> GetAsync(Guid id);

        Task<Invitation> CreateAsync(Invitation invitation);
    }
}