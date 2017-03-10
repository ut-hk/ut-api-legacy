using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Activities.Managers
{
    public class ActivityPlanTimeSlotManager : IActivityPlanTimeSlotManager
    {
        private readonly IRepository<ActivityPlanTimeSlot, long> _activityPlanTimeSlotRepository;

        public ActivityPlanTimeSlotManager(
            IRepository<ActivityPlanTimeSlot, long> activityPlanTimeSlotRepository)
        {
            _activityPlanTimeSlotRepository = activityPlanTimeSlotRepository;
        }

        public async Task<ActivityPlanTimeSlot> GetAsync(long id)
        {
            var activityPlanTimeSlot = await _activityPlanTimeSlotRepository.FirstOrDefaultAsync(id);

            if (activityPlanTimeSlot == null)
                throw new UserFriendlyException($"The activity plan time slot with id = {id} does not exist.");

            return activityPlanTimeSlot;
        }

        public async Task<ActivityPlanTimeSlot> CreateAsync(ActivityPlanTimeSlot activityPlanTimeSlot)
        {
            activityPlanTimeSlot.Id = await _activityPlanTimeSlotRepository.InsertAndGetIdAsync(activityPlanTimeSlot);

            return activityPlanTimeSlot;
        }

        public void EditActivityPlanTimeSlot(ActivityPlanTimeSlot activityPlanTimeSlot, DateTime startTime, DateTime endTime, long editUserId)
        {
            activityPlanTimeSlot.Edit(startTime, endTime, editUserId);
        }

        public async Task RemoveAsync(ActivityPlanTimeSlot activityPlanTimeSlot)
        {
            await _activityPlanTimeSlotRepository.DeleteAsync(activityPlanTimeSlot);
        }
    }
}