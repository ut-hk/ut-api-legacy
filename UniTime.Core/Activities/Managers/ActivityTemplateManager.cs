using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Locations;
using UniTime.Tags;

namespace UniTime.Activities.Managers
{
    public class ActivityTemplateManager : IActivityTemplateManager
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;

        public ActivityTemplateManager(
            IRepository<AbstractActivity, Guid> abstractActivityRepository)
        {
            _abstractActivityRepository = abstractActivityRepository;
        }

        public async Task<ActivityTemplate> GetAsync(Guid id)
        {
            var activityTemplate = await _abstractActivityRepository.FirstOrDefaultAsync(id) as ActivityTemplate;

            if (activityTemplate == null)
                throw new UserFriendlyException("The activity template with id = " + id + " does not exist.");

            return activityTemplate;
        }

        public async Task<ActivityTemplate> CreateAsync(ActivityTemplate activityTemplate)
        {
            activityTemplate.Id = await _abstractActivityRepository.InsertAndGetIdAsync(activityTemplate);

            return activityTemplate;
        }

        public void EditActivityTemplate(ActivityTemplate activityTemplate, string name, string description, ICollection<ActivityTemplateReferenceTimeSlot> referenceTimeSlots, Location location, ICollection<Tag> tags, long editUserId)
        {
            activityTemplate.Edit(name, description, location, tags, editUserId);
            activityTemplate.Edit(referenceTimeSlots, editUserId);
        }
    }
}