using System;
using System.Linq;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using UniTime.AbstractActivities.Dtos;
using UniTime.Activities;
using UniTime.Descriptions.Enums;

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

                    mapper.CreateMap<Activity, ActivityListDto>()
                        .ForMember(
                            activityListDto => activityListDto.CoverTextDescription,
                            configuration => configuration.MapFrom(member => member.Descriptions.FirstOrDefault(description => description.Type == DescriptionType.Text && description.Content.Length > 1)))
                        .ForMember(
                            activityListDto => activityListDto.CoverImageDescription,
                            configuration => configuration.MapFrom(member => member.Descriptions.FirstOrDefault(description => description.Type == DescriptionType.ExternalImage || description.Type == DescriptionType.InternalImage)));

                    mapper.CreateMap<ActivityTemplate, ActivityTemplateListDto>()
                        .ForMember(
                            activityTemplateListDto => activityTemplateListDto.CoverTextDescription,
                            configuration => configuration.MapFrom(member => member.Descriptions.FirstOrDefault(description => description.Type == DescriptionType.Text && description.Content.Length > 1)))
                        .ForMember(
                            activityTemplateListDto => activityTemplateListDto.CoverImageDescription,
                            configuration => configuration.MapFrom(member => member.Descriptions.FirstOrDefault(description => description.Type == DescriptionType.ExternalImage || description.Type == DescriptionType.InternalImage)))
                        .ForMember(
                            activityTemplate => activityTemplate.StartTime,
                            configuration => configuration.MapFrom(member => member.ReferenceTimeSlots.FirstOrDefault(referenceTimeSlot => referenceTimeSlot.StartTime > DateTime.UtcNow).StartTime));
                });

            Configuration.Caching.Configure("UserCache", cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30); });
            Configuration.Caching.Configure("GuestIdCache", cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromDays(1); });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}