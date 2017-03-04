using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Activities.Managers
{
    public interface IActivityPlanTimeSlotManager : IDomainService
    {
        Task<ActivityPlanTimeSlot> GetAsync(long id);

        Task<ActivityPlanTimeSlot> CreateAsync(ActivityPlanTimeSlot activityPlanTimeSlot);

        void UpdateTimes(ActivityPlanTimeSlot activityPlanTimeSlot, DateTime startTime, DateTime endTime);

        Task RemoveAsync(ActivityPlanTimeSlot activityPlanTimeSlot);
    }
}