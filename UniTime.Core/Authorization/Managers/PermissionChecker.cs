using Abp.Authorization;
using UniTime.Authorization.Roles;
using UniTime.MultiTenancy;
using UniTime.Users;
using UniTime.Users.Managers;

namespace UniTime.Authorization.Managers
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
