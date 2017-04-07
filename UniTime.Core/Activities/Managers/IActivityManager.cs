using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using UniTime.Locations;
using UniTime.Tags;

namespace UniTime.Activities.Managers
{
    public interface IActivityManager : IDomainService
    {
        string DoesNotExistMessage { get; }

        Task<Activity> GetAsync(Guid id);

        Task<Activity> CreateAsync(Activity activity);

        void EditActivity(Activity activity, string name, DateTime? startTime, DateTime? endTime, Location location, ICollection<Tag> tags, long editUserId);
        void EditDescriptions(Activity activity, long[] descriptionIds, long editUserId);
    }
}