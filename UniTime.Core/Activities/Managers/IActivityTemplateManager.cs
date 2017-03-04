using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Activities.Managers
{
    public interface IActivityTemplateManager : IDomainService
    {
        Task<ActivityTemplate> GetAsync(Guid id);

        Task<ActivityTemplate> CreateAsync(ActivityTemplate activityTemplate);
    }
}