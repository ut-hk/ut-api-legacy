using UniTime.EntityFramework;
using EntityFramework.DynamicFilters;

namespace UniTime.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly UniTimeDbContext _context;

        public InitialHostDbBuilder(UniTimeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
