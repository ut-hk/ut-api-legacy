using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using UniTime.Analysis.Managers;
using UniTime.Files;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    public class UserAppService : UniTimeAppServiceBase, IUserAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<File, Guid> _fileRepository;
        private readonly IGuestManager _guestManager;

        public UserAppService(
            IRepository<File, Guid> fileRepository,
            IGuestManager guestManager,
            ICacheManager cacheManager)
        {
            _fileRepository = fileRepository;
            _guestManager = guestManager;
            _cacheManager = cacheManager;
        }

        public async Task<GetUserOutput> GetUser(EntityDto<long> input)
        {
            var user = await _cacheManager
                .GetCache("UserCache")
                .GetAsync(input.Id, () => GetUserFromDatabase(input.Id));

            return new GetUserOutput
            {
                User = user.MapTo<UserDto>()
            };
        }

        [AbpAuthorize]
        [DisableAuditing]
        public async Task<GetMyUserOutput> GetMyUser()
        {
            var currentUser = await GetCurrentUserAsync();
            var guestId = await _cacheManager
                .GetCache("GuestIdCache")
                .GetAsync(currentUser.Id, () => GetGuestIdFromDatabase(currentUser.Id));

            return new GetMyUserOutput
            {
                MyUser = currentUser.MapTo<UserDto>(),
                GuestId = guestId
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


        private async Task<User> GetUserFromDatabase(long id)
        {
            return await UserManager.FindByIdAsync(id);
        }

        private async Task<Guid> GetGuestIdFromDatabase(long userId)
        {
            var guest = await _guestManager.GetByUserIdAsync(userId);

            return guest.Id;
        }
    }
}