using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using UniTime.Locations;
using UniTime.Tags;

namespace UniTime.Activities.Managers
{
    public interface IActivityTemplateManager : IDomainService
    {
        Task<ActivityTemplate> GetAsync(Guid id);

        Task<ActivityTemplate> CreateAsync(ActivityTemplate activityTemplate);

        void EditActivityTemplate(ActivityTemplate activityTemplate, string name, string description, ICollection<ActivityTemplateReferenceTimeSlot> referenceTimeSlots, Location location, ICollection<Tag> tags, long editUserId);
    }
}