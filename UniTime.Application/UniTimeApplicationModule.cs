using System;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace UniTime
{
    [DependsOn(
        typeof(UniTimeCoreModule),
        typeof(AbpAutoMapperModule))]
    public class UniTimeApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ApplicationModuleAuthorizationModule>();

            Configuration.Modules.AbpAutoMapper()
                .Configurators.Add(mapper =>
                {
                    // Add your custom AutoMapper mappings here...
                    // mapper.CreateMap<,>()
                });

            Configuration.Caching.Configure("UserCache", cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30); });
            Configuration.Caching.Configure("GuestIdCache", cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromDays(1); });
            Configuration.Caching.Configure("NumberOfFriendsCache", cache=> { cache.DefaultSlidingExpireTime = TimeSpan.FromSeconds(30); });
            Configuration.Caching.Configure("CoverImageDescriptionCache", cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromDays(1); });
            Configuration.Caching.Configure("CoverTextDescriptionCache", cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromDays(1); });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}