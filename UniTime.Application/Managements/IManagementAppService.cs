using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Managements.Dtos;

namespace UniTime.Managements
{
    public interface IManagementAppService : IApplicationService
    {
        Task<GetUsersOutput> GetAllUsers(GetUsersInput input);

        Task UpdateRolePermissions(UpdateRolePermissionsInput input);

        Task RemoveUser(EntityDto<long> input);
    }
}