using System.Linq;
using Microsoft.AspNet.Identity;
using UniTime.EntityFramework;
using UniTime.MultiTenancy;
using UniTime.Users;

namespace UniTime.Migrations.SeedData
{
    public class DefaultUserCreator
    {
        private readonly UniTimeDbContext _context;

        public DefaultUserCreator(UniTimeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUser();
        }

        private void CreateUser()
        {
            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            var defaultUser = _context.Users.FirstOrDefault(e => e.Name == "Leo");

            if (defaultUser == null)
            {
                var user = new User
                {
                    TenantId = defaultTenant.Id,
                    Name = "Leo",
                    Surname = "Choi",
                    EmailAddress = "choimankin@gmail.com",
                    UserName = "leochoi",
                    Password = new PasswordHasher().HashPassword("12345678"),
                    IsActive = true
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }
    }
}