using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Users.Managers
{
    public interface ITrackManager : IDomainService
    {
        IQueryable<User> GetTrackingUsersAsync(long targetUserId);
        IQueryable<User> GetTrackedByUsersAsync(long targetUserId);
        IQueryable<User> GetInterTrackingUsersAsync(long targetUserId);

        Task TrackAsync(User targetUser, User user);
    }
}