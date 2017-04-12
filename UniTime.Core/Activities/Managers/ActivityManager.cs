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

        public string DoesNotExistMessage => "The activity does not exist.";

        public async Task<Activity> GetAsync(Guid id)
        {
            var activity = await _abstractActivityRepository.FirstOrDefaultAsync(id) as Activity;

            if (activity == null)
                throw new UserFriendlyException(DoesNotExistMessage);

            return activity;
        }

        public async Task<Activity> CreateAsync(Activity activity)
        {
            activity.Id = await _abstractActivityRepository.InsertAndGetIdAsync(activity);

            return activity;
        }

        public void EditActivity(Activity activity, string name, DateTime? startTime, DateTime? endTime, Location location, ICollection<Tag> tags, long editUserId)
        {
            activity.Edit(name, location, tags, editUserId);
            activity.Edit(startTime, endTime, editUserId);
        }

        public void EditDescriptions(Activity activity, long[] descriptionIds, long editUserId)
        {
            var activityDescriptions = activity.Descriptions;

            foreach (var activityDescription in activityDescriptions)
                for (var i = 0; i < descriptionIds.Length; i++)
                    if (descriptionIds[i] == activityDescription.Id)
                        activityDescription.EditPriority(i, editUserId);
        }

        public async Task RemoveAsync(Activity activity, long deleteUserId)
        {
            await activity.RemoveAsync(_abstractActivityRepository, deleteUserId);
        }
    }
}