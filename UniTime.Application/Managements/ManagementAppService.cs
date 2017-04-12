using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using UniTime.Authorization.Roles;
using UniTime.Managements.Dtos;
using UniTime.Users;
using UniTime.Users.Dtos;

namespace UniTime.Managements
{
    public class ManagementAppService : UniTimeAppServiceBase, IManagementAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;

        public ManagementAppService(
            IRepository<User, long> userRepository,
            RoleManager roleManager, 
            IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _permissionManager = permissionManager;
        }

        public async Task<GetUsersOutput> GetAllUsers(GetUsersInput input)
        {
            if (!PermissionChecker.IsGranted("Management.User.GetAllUsers"))
            {
                throw new AbpAuthorizationException("You are not authorized to get all users.");
            }

            var queryNames = input.QueryNames?.Split(' ').Where(queryKeyword => queryKeyword.Length > 0).ToArray();

            var users = await _userRepository.GetAll()
                .WhereIf(input.QueryNames != null, u => queryNames.Any(queryName => u.Name.Contains(queryName)))
                .ProjectTo<UserDto>()
                .ToListAsync();

            return new GetUsersOutput
            {
                Users = users
            };
        }

        public async Task RemoveUser(EntityDto<long> input)
        {
            if (!PermissionChecker.IsGranted("Management.User.RemoveUser"))
            {
                throw new AbpAuthorizationException("You are not authorized to remove this user.");
            }

            var user = await UserManager.FindByIdAsync(input.Id);

            await _userRepository.DeleteAsync(user);
        }

        public async Task UpdateRolePermissions(UpdateRolePermissionsInput input)
        {
            throw new AbpAuthorizationException("You are not authorized to update role permissions.");

            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            var grantedPermissions = _permissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }

    }
}