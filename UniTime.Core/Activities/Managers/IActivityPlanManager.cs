using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Activities.Managers
{
    public interface IActivityPlanManager : IDomainService
    {
        Task<ActivityPlan> GetActivityPlanAsync(Guid id);
        Task<ActivityPlan> CreateActivityPlanAsync(ActivityPlan activityPlan);
    }
}