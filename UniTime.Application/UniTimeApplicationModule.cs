using System;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace UniTime
{
    [DependsOn(typeof(UniTimeCoreModule), typeof(AbpAutoMapperModule))]
    public class UniTimeApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper()
                .Configurators.Add(mapper =>
                {
                    // Add your custom AutoMapper mappings here...
                    // mapper.CreateMap<,>()
                });

            Configuration.Caching.Configure("LongCache", cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromHours(4); });
            Configuration.Caching.Configure("ShortCache", cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30); });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}