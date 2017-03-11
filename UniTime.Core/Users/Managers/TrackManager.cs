using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace UniTime.Users.Managers
{
    public class TrackManager : ITrackManager
    {
        private readonly IRepository<Track, long> _trackRepository;

        public TrackManager(
            IRepository<Track, long> trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<ICollection<User>> GetTrackingUsersAsync(long targetUserId)
        {
            var trackingUsers = await _trackRepository.GetAll()
                .Where(track => track.FromId == targetUserId)
                .Select(track => track.To)
                .ToListAsync();

            return trackingUsers;
        }

        public async Task<ICollection<User>> GetTrackedByUsersAsync(long targetUserId)
        {
            var trackedByUsers = await _trackRepository.GetAll()
                .Where(track => track.ToId == targetUserId)
                .Select(track => track.From)
                .ToListAsync();

            return trackedByUsers;
        }

        public async Task<ICollection<User>> GetFriendsAsync(long targetUserId)
        {
            var friends = await _trackRepository.GetAll()
                .Where(track => track.FromId == targetUserId)
                .SelectMany(track => track.To.Trackings)
                .Where(track => track.ToId == targetUserId)
                .Select(track => track.From)
                .ToListAsync();

            return friends;
        }

        public async Task TrackAsync(User targetUser, User user)
        {
            await _trackRepository.InsertAsync(Track.Create(user, targetUser));
        }
    }
}