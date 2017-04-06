using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper.QueryableExtensions;
using UniTime.Users.Dtos;
using UniTime.Users.Managers;

namespace UniTime.Users
{
    public class RelationshipAppService : UniTimeAppServiceBase, IRelationshipAppService
    {
        private readonly IRepository<FriendPair, long> _friendPairRepository;
        private readonly ITrackManager _trackManager;

        public RelationshipAppService(
            ITrackManager trackManager,
            IRepository<FriendPair, long> friendPairRepository)
        {
            _trackManager = trackManager;
            _friendPairRepository = friendPairRepository;
        }

        public async Task<GetTrackingUsersOutput> GetTrackingUsers(TargetUserInput input)
        {
            var trackingUsers = await _trackManager
                .GetTrackingUsersAsync(input.TargetUserId)
                .ProjectTo<UserListDto>()
                .ToListAsync();

            return new GetTrackingUsersOutput
            {
                TrackingUsers = trackingUsers
            };
        }

        public async Task<GetTrackedByUsersOutput> GetTrackedByUsers(TargetUserInput input)
        {
            var trackedByUsers = await _trackManager
                .GetTrackedByUsersAsync(input.TargetUserId)
                .ProjectTo<UserListDto>()
                .ToListAsync();

            return new GetTrackedByUsersOutput
            {
                TrackedByUsers = trackedByUsers
            };
        }

        public async Task<GetInterTrackingUsersOutput> GetInterTrackingUsers(TargetUserInput input)
        {
            var interTrackingUsers = await _trackManager
                .GetInterTrackingUsersAsync(input.TargetUserId)
                .ProjectTo<UserListDto>()
                .ToListAsync();

            return new GetInterTrackingUsersOutput
            {
                InterTrackingUsers = interTrackingUsers
            };
        }

        public async Task<GetFriendsOutput> GetFriends(TargetUserInput input)
        {
            var friends = await _friendPairRepository.GetAll()
                .Where(fp => fp.LeftId == input.TargetUserId || fp.RightId == input.TargetUserId)
                .Select(fp => new[] {fp.Left, fp.Right})
                .SelectMany(user => user)
                .Where(user => user.Id != input.TargetUserId)
                .Distinct()
                .ProjectTo<UserListDto>()
                .ToListAsync();

            return new GetFriendsOutput
            {
                Friends = friends
            };
        }

        [AbpAuthorize]
        public async Task Track(TargetUserInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetUser = await UserManager.GetUserByIdAsync(input.TargetUserId);

            await _trackManager.TrackAsync(targetUser, currentUser);
        }
    }
}