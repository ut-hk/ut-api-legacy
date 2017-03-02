using System.Linq;
using UniTime.EntityFramework;
using UniTime.MultiTenancy;

namespace UniTime.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly UniTimeDbContext _context;

        public DefaultTenantCreator(UniTimeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            // Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
