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

        public IQueryable<User> GetTrackingUsersAsync(long targetUserId)
        {
            var trackingUsers = _trackRepository.GetAll()
                .Where(track => track.FromId == targetUserId)
                .Select(track => track.To);

            return trackingUsers;
        }

        public IQueryable<User> GetTrackedByUsersAsync(long targetUserId)
        {
            var trackedByUsers = _trackRepository.GetAll()
                .Where(track => track.ToId == targetUserId)
                .Select(track => track.From);

            return trackedByUsers;
        }

        public IQueryable<User> GetInterTrackingUsersAsync(long targetUserId)
        {
            var friends = _trackRepository.GetAll()
                .Where(track => track.FromId == targetUserId)
                .SelectMany(track => track.To.Trackings)
                .Where(track => track.ToId == targetUserId)
                .Select(track => track.From);

            return friends;
        }

        public async Task TrackAsync(User targetUser, User user)
        {
            await _trackRepository.InsertAsync(Track.Create(user, targetUser));
        }
    }
}