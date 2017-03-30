using System;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Analysis.Managers;
using UniTime.Files;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    [AbpAuthorize]
    public class UserAppService : UniTimeAppServiceBase, IUserAppService
    {
        private readonly IRepository<File, Guid> _fileRepository;
        private readonly IGuestManager _guestManager;

        public UserAppService(
            IRepository<File, Guid> fileRepository,
            IGuestManager guestManager)
        {
            _fileRepository = fileRepository;
            _guestManager = guestManager;
        }

        [DisableAuditing]
        public async Task<GetMyUserOutput> GetMyUser()
        {
            var currentUser = await GetCurrentUserAsync();
            var guest = await _guestManager.GetByUserIdAsync(currentUser.Id);

            return new GetMyUserOutput
            {
                MyUser = currentUser.MapTo<UserDto>(),
                GuestId = guest.Id
            };
        }

        public async Task UpdateMyUser(UpdateMyUserInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var cover = input.CoverId.HasValue ? await _fileRepository.GetAsync(input.CoverId.Value) as Image : null;

            UserManager.EditUser(currentUser, input.Name, input.Surname, input.PhoneNumber, input.Gender, input.Birthday, cover);
        }
    }
}