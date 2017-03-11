using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Users.Managers
{
    public interface ITrackManager : IDomainService
    {
        Task<ICollection<User>> GetTrackingUsersAsync(long targetUserId);
        Task<ICollection<User>> GetTrackedByUsersAsync(long targetUserId);
        Task<ICollection<User>> GetFriendsAsync(long targetUserId);

        Task TrackAsync(User targetUser, User user);
    }
}