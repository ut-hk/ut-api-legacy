using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using UniTime.Tags;

namespace UniTime.Activities.Managers
{
    public interface IActivityPlanManager : IDomainService
    {
        string DoesNotExistMessage { get; }

        Task<ActivityPlan> GetAsync(Guid id);

        Task<ActivityPlan> CreateAsync(ActivityPlan activityPlan);

        void EditActivityPlan(ActivityPlan activityPlan, string name, ICollection<Tag> tags, long editUserId);
        void EditDescriptions(ActivityPlan activityPlan, long[] descriptionIds, long editUserId);

        Task RemoveAsync(ActivityPlan activityPlan, long deleteUserId);
    }
}