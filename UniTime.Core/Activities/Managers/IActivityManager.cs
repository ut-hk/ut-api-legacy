using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Activities.Managers
{
    public interface IActivityManager : IDomainService
    {
        Task<Activity> GetAsync(Guid id);

        Task<Activity> CreateAsync(Activity activity);

        void EditActivity(Activity activity, string name, string description, long editUserId);
    }
}