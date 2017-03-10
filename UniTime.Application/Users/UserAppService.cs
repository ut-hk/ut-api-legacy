using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    [AbpAuthorize]
    public class UserAppService : UniTimeAppServiceBase, IUserAppService
    {
        [DisableAuditing]
        public async Task<GetMyUserOutput> GetMyUser()
        {
            return new GetMyUserOutput
            {
                MyUser = (await GetCurrentUserAsync()).MapTo<UserDto>()
            };
        }
    }
}