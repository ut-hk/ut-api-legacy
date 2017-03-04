using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Activities.Managers
{
    public interface IActivityPlanManager : IDomainService
    {
        Task<ActivityPlan> GetAsync(Guid id);

        Task<ActivityPlan> CreateAsync(ActivityPlan activityPlan);
    }
}