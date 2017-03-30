using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.OData;
using Abp.WebApi.OData.Configuration;
using Swashbuckle.Application;
using UniTime.Activities;

namespace UniTime.Api
{
    [DependsOn(
        typeof(AbpWebApiModule),
        typeof(UniTimeApplicationModule),
        typeof(AbpWebApiODataModule))]
    public class UniTimeWebApiModule : AbpModule
    {
        public override void PreInitialize()
        {
            var builder = Configuration.Modules.AbpWebApiOData().ODataModelBuilder;

            builder.EntitySet<ActivityTemplate>("ActivityTemplate");
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi()
                .DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(UniTimeApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            ConfigureSwaggerUi();
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi()
                .HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "UniTime.WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi();
        }
    }
}