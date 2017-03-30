using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Analysis.Managers
{
    public interface IGuestManager : IDomainService
    {
        Task<Guest> GetAnonymousGuestAsync(Guid id);
        Task<Guest> GetByUserIdAsync(long userId);

        Task<Guest> CreateAsync(long? ownerId = null);

        void MergeGuests(Guest guest, Guest anonymousGuest);
    }
}