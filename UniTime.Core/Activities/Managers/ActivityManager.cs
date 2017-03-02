using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Activities.Managers
{
    public class ActivityManager : IActivityManager
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;

        public ActivityManager(
            IRepository<AbstractActivity, Guid> abstractActivityRepository)
        {
            _abstractActivityRepository = abstractActivityRepository;
        }

        public async Task<Activity> GetActivityAsync(Guid id)
        {
            var activity = await _abstractActivityRepository.FirstOrDefaultAsync(id) as Activity;

            if (activity == null) throw new UserFriendlyException("The activity with id = " + id + " does not exist.");

            return activity;
        }

        public async Task<Activity> CreateActivityAsync(Activity activity)
        {
            activity.Id =  await _abstractActivityRepository.InsertAndGetIdAsync(activity);

            return activity;
        }
    }
}