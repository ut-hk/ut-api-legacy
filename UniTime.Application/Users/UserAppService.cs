using System.Threading.Tasks;
using Abp.AutoMapper;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    public class UserAppService : UniTimeAppServiceBase, IUserAppService
    {
        public async Task<GetMyUserOutput> GetMyUser()
        {
            return new GetMyUserOutput
            {
                MyUser = (await GetCurrentUserAsync()).MapTo<UserDto>()
            };
        }
    }
}