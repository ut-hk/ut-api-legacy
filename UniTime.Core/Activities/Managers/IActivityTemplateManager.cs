using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Activities.Managers
{
    public interface IActivityTemplateManager : IDomainService
    {
        Task<ActivityTemplate> GetActivityTemplateAsync(Guid id);
        Task<ActivityTemplate> CreateActivityTemplateAsync(ActivityTemplate activityTemplate);
    }
}