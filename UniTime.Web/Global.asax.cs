using System;
using System.Diagnostics;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using Microsoft.ApplicationInsights.Extensibility;
using SqlServerTypes;

namespace UniTime.Web
{
    public class MvcApplication : AbpWebApplication<UniTimeWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            DisableApplicationInsightsOnDebug();

            Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));

            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            base.Application_Start(sender, e);
        }

        [Conditional("DEBUG")]
        private static void DisableApplicationInsightsOnDebug()
        {
            TelemetryConfiguration.Active.DisableTelemetry = true;
        }
    }
}