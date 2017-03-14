using System;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Files;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    [AbpAuthorize]
    public class UserAppService : UniTimeAppServiceBase, IUserAppService
    {
        private readonly IRepository<File, Guid> _fileRepository;

        public UserAppService(
            IRepository<File, Guid> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [DisableAuditing]
        public async Task<GetMyUserOutput> GetMyUser()
        {
            return new GetMyUserOutput
            {
                MyUser = (await GetCurrentUserAsync()).MapTo<UserDto>()
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