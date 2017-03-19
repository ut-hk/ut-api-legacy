using System;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Analysis.Managers
{
    public class GuestManager : IGuestManager
    {
        private readonly IRepository<Guest, Guid> _guestRepository;

        public GuestManager(
            IRepository<Guest, Guid> guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<Guest> GetAsync(Guid id)
        {
            var guest = await _guestRepository.FirstOrDefaultAsync(id);

            if (guest == null) throw new UserFriendlyException($"The guest with id = {id} does not exist.");

            return guest;
        }

        public async Task<Guest> GetByUserIdAsync(long userId)
        {
            var guest = await _guestRepository.FirstOrDefaultAsync(g => g.OwnerId == userId) ?? await CreateAsync(userId);

            return guest;
        }

        public async Task<Guest> CreateAsync(long? ownerId = null)
        {
            var guest = Guest.Create(ownerId);

            guest.Id = await _guestRepository.InsertAndGetIdAsync(guest);

            return guest;
        }

  
        public void MergeWithOwner(Guest guest, long ownerId)
        {
            guest.EditOwner(ownerId);
        }
    }
}