namespace UniTime.Managements.Dtos
{
    public class UpdateRolePermissionsInput
    {
        public int RoleId { get; set; }

        public string[] GrantedPermissionNames { get; set; }
    }
}