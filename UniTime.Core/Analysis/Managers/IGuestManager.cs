using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Analysis.Managers
{
    public interface IGuestManager : IDomainService
    {
        Task<Guest> GetAsync(Guid id);
        Task<Guest> GetByUserIdAsync(long userId);

        Task<Guest> CreateAsync(long? ownerId = null);

        void MergeWithOwner(Guest guest, long ownerId);
    }
}