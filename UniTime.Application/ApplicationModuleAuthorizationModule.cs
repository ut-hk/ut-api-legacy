using Abp.Authorization;

namespace UniTime
{
    public class ApplicationModuleAuthorizationModule : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var management = context.CreatePermission("Management");

            var userManagement = management.CreateChildPermission("Management.User");
            userManagement.CreateChildPermission("Management.User.GetAllUsers");
            userManagement.CreateChildPermission("Management.User.RemoveUser");
        }

        // ["Management","Management.User","Management.User.GetAllUsers","Management.User.RemoveUser"]

    }
}