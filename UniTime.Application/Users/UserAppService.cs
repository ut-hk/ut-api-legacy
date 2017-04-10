using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using AutoMapper.QueryableExtensions;
using UniTime.Analysis.Managers;
using UniTime.Files;
using UniTime.Invitations;
using UniTime.Invitations.Enums;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    public class UserAppService : UniTimeAppServiceBase, IUserAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<File, Guid> _fileRepository;
        private readonly IRepository<FriendPair, long> _friendPairRepository;
        private readonly IGuestManager _guestManager;
        private readonly IRepository<Invitation, Guid> _invitationRepository;
        private readonly IRepository<User, long> _userRepository;

        public UserAppService(
            IRepository<File, Guid> fileRepository,
            IRepository<User, long> userRepository,
            IRepository<Invitation, Guid> invitationRepository,
            IRepository<FriendPair, long> friendPairRepository,
            IGuestManager guestManager,
            ICacheManager cacheManager)
        {
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _invitationRepository = invitationRepository;
            _friendPairRepository = friendPairRepository;
            _guestManager = guestManager;
            _cacheManager = cacheManager;
        }

        public async Task<GetUserOutput> GetUser(EntityDto<long> input)
        {
            var currentUserId = AbpSession.UserId;

            var user = await _cacheManager
                .GetCache("UserDtoCache")
                .GetAsync(input.Id, () => GetUserDtoFromDatabase(input.Id));
            var numberOfFriends = await _cacheManager
                .GetCache("NumberOfFriendsCache")
                .GetAsync(input.Id, () => GetNumberOfFriendsFromDatabase(input.Id));
            var isFriend = currentUserId.HasValue && currentUserId != input.Id &&
                           await _friendPairRepository.GetAll()
                               .AnyAsync(fp =>
                                   (fp.LeftId == user.Id || fp.LeftId == currentUserId) &&
                                   (fp.RightId == user.Id || fp.RightId == currentUserId));
            var hasInvited = currentUserId.HasValue && currentUserId != input.Id &&
                             await _invitationRepository.GetAll()
                                 .OfType<FriendInvitation>()
                                 .AnyAsync(fi =>
                                     fi.OwnerId == currentUserId.Value && fi.InviteeId == input.Id &&
                                     fi.Status == InvitationStatus.Pending);

            return new GetUserOutput
            {
                User = user,
                NumberOfFriends = numberOfFriends,
                IsFriend = isFriend,
                HasInvited = hasInvited
            };
        }

        [AbpAuthorize]
        [DisableAuditing]
        public async Task<GetMyUserOutput> GetMyUser()
        {
            var currentUserId = GetCurrentUserId();

            var currentUser = await GetUserDtoFromDatabase(currentUserId);
            var guestId = await _cacheManager
                .GetCache("GuestIdCache")
                .GetAsync(currentUserId, () => GetGuestIdFromDatabase(currentUserId));
            var numberOfPendingActvityInvitations = await _invitationRepository
                .CountAsync(invitation => invitation is ActivityInvitation &&
                                          invitation.InviteeId == currentUserId &&
                                          invitation.Status == InvitationStatus.Pending);
            var numberOfPendingFriendInvitations = await _invitationRepository
                .CountAsync(invitation => invitation is ActivityInvitation &&
                                          invitation.InviteeId == currentUserId &&
                                          invitation.Status == InvitationStatus.Pending);
            var numberOfFriends = await _cacheManager
                .GetCache("NumberOfFriendsCache")
                .GetAsync(currentUserId, () => GetNumberOfFriendsFromDatabase(currentUserId));

            return new GetMyUserOutput
            {
                MyUser = currentUser,
                GuestId = guestId,
                NumberOfActivityInvitations = numberOfPendingActvityInvitations,
                NumberOfFriendInvitations = numberOfPendingFriendInvitations,
                NumberOfFriends = numberOfFriends
            };
        }

        [AbpAuthorize]
        public async Task UpdateMyUserPassword(UpdateMyUserPasswordInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            UserManager.EditPassword(currentUser, input.OldPassword, input.NewPassword);
        }

        [AbpAuthorize]
        public async Task UpdateMyUser(UpdateMyUserInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var icon = input.IconId.HasValue ? await _fileRepository.GetAsync(input.IconId.Value) as Image : null;
            var cover = input.CoverId.HasValue ? await _fileRepository.GetAsync(input.CoverId.Value) as Image : null;

            UserManager.EditUser(currentUser, input.Name, input.Surname, input.PhoneNumber, input.Gender, input.Birthday, icon, cover);
        }


        private async Task<UserDto> GetUserDtoFromDatabase(long id)
        {
            var user = await _userRepository.GetAll()
                .ProjectTo<UserDto>()
                .FirstAsync(u => u.Id == id);

            return user;
        }

        private async Task<Guid> GetGuestIdFromDatabase(long userId)
        {
            var guest = await _guestManager.GetByUserIdAsync(userId);

            return guest.Id;
        }

        private async Task<int> GetNumberOfFriendsFromDatabase(long userId)
        {
            var numberOfFriends = await _friendPairRepository.GetAll()
                .Where(fp => fp.LeftId == userId || fp.RightId == userId)
                .Select(fp => new[] {fp.Left, fp.Right})
                .SelectMany(user => user)
                .Where(user => user.Id != userId)
                .Distinct()
                .CountAsync();

            return numberOfFriends;
        }
    }
}