using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Activities.Managers
{
    public class ActivityPlanManager : IActivityPlanManager
    {
        private readonly IRepository<ActivityPlan, Guid> _activityPlanRepository;

        public ActivityPlanManager(
            IRepository<ActivityPlan, Guid> activityPlanRepository)
        {
            _activityPlanRepository = activityPlanRepository;
        }

        public async Task<ActivityPlan> GetAsync(Guid id)
        {
            var activityPlan = await _activityPlanRepository.FirstOrDefaultAsync(id);

            if (activityPlan == null) throw new UserFriendlyException("The activity plan with id = " + id + " does not exist.");

            return activityPlan;
        }

        public async Task<ActivityPlan> CreateAsync(ActivityPlan activityPlan)
        {
            activityPlan.Id = await _activityPlanRepository.InsertAndGetIdAsync(activityPlan);

            return activityPlan;
        }

        public void EditDescriptions(ActivityPlan activityPlan, long[] descriptionIds, long editUserId)
        {
            if (activityPlan.OwnerId != editUserId)
                throw new UserFriendlyException($"You are not allowed to update this activity plan with id = {activityPlan.Id}.");

            var activityPlanDescriptions = activityPlan.Descriptions;

            foreach (var activityPlanDescription in activityPlanDescriptions)
                for (var i = 0; i < descriptionIds.Length; i++)
                    if (descriptionIds[i] == activityPlanDescription.Id)
                        activityPlanDescription.EditPriority(i);
        }
    }
}