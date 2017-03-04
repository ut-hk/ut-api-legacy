﻿using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using SqlServerTypes;

namespace UniTime.Web
{
    public class MvcApplication : AbpWebApplication<UniTimeWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));

            base.Application_Start(sender, e);
        }
    }
}