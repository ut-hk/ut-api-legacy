using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Activities.Managers
{
    public interface IActivityManager : IDomainService
    {
        Task<Activity> GetActivityAsync(Guid id);
        Task<Activity> CreateActivityAsync(Activity activity);
    }
}