using System.Threading.Tasks;
using Abp.Application.Services;
using UniTime.Sessions.Dtos;

namespace UniTime.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}