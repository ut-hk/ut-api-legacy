using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Locations;
using UniTime.Tags;

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

        public async Task<Activity> GetAsync(Guid id)
        {
            var activity = await _abstractActivityRepository.FirstOrDefaultAsync(id) as Activity;

            if (activity == null)
                throw new UserFriendlyException("The activity with id = " + id + " does not exist.");

            return activity;
        }

        public async Task<Activity> CreateAsync(Activity activity)
        {
            activity.Id = await _abstractActivityRepository.InsertAndGetIdAsync(activity);

            return activity;
        }

        public void EditActivity(Activity activity, string name, string description, DateTime? startTime, DateTime? endTime, Location location, ICollection<Tag> tags, long editUserId)
        {
            activity.Edit(name, description, location, tags, editUserId);
            activity.Edit(startTime, endTime, editUserId);
        }
    }
}