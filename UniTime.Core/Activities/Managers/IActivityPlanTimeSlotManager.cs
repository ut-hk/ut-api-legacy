using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Activities.Managers
{
    public interface IActivityPlanTimeSlotManager : IDomainService
    {
        Task<ActivityPlanTimeSlot> GetAsync(long id);

        Task<ActivityPlanTimeSlot> CreateAsync(ActivityPlanTimeSlot activityPlanTimeSlot);

        Task RemoveAsync(ActivityPlanTimeSlot activityPlanTimeSlot);
    }
}