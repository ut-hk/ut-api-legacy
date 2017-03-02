using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using UniTime.EntityFramework;

namespace UniTime.Migrator
{
    [DependsOn(typeof(UniTimeDataModule))]
    public class UniTimeMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<UniTimeDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}