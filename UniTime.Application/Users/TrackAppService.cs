using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using UniTime.Users.Dtos;
using UniTime.Users.Managers;

namespace UniTime.Users
{
    [AbpAuthorize]
    public class TrackAppService : UniTimeAppServiceBase, ITrackAppService
    {
        private readonly ITrackManager _trackManager;

        public TrackAppService(
            ITrackManager trackManager)
        {
            _trackManager = trackManager;
        }

        public async Task<GetTrackingUsersOutput> GetTrackingUsers(TargetUserInput input)
        {
            var trackingUsers = await _trackManager.GetTrackingUsersAsync(input.TargetUserId);

            return new GetTrackingUsersOutput
            {
                TrackingUsers = trackingUsers.MapTo<List<UserListDto>>()
            };
        }

        public async Task<GetTrackedByUsersOutput> GetTrackedByUsers(TargetUserInput input)
        {
            var trackedByUsers = await _trackManager.GetTrackedByUsersAsync(input.TargetUserId);

            return new GetTrackedByUsersOutput
            {
                TrackedByUsers = trackedByUsers.MapTo<List<UserListDto>>()
            };
        }

        public async Task<GetFriendsOutput> GetFriends(TargetUserInput input)
        {
            var friends = await _trackManager.GetFriendsAsync(input.TargetUserId);

            return new GetFriendsOutput
            {
                Friends = friends.MapTo<List<UserListDto>>()
            };
        }

        public async Task Track(TargetUserInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetUser = await UserManager.GetUserByIdAsync(input.TargetUserId);

            await _trackManager.TrackAsync(targetUser, currentUser);
        }
    }
}