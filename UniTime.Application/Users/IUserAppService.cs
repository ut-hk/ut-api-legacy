using System.Threading.Tasks;
using Abp.Application.Services;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<GetMyUserOutput> GetMyUser();
    }
}