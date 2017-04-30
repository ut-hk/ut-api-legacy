using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public string DoesNotExistMessage => "The activity template does not exist.";

        public async Task<ActivityTemplate> GetAsync(Guid id)
        {
            var activityTemplate = await _abstractActivityRepository.FirstOrDefaultAsync(id) as ActivityTemplate;

            if (activityTemplate == null)
                throw new UserFriendlyException(DoesNotExistMessage);

            return activityTemplate;
        }

        public async Task<ActivityTemplate> CreateAsync(ActivityTemplate activityTemplate)
        {
            if (activityTemplate.ReferenceId != null)
            {
                var activityTemplateWithReferenceId = await _abstractActivityRepository.GetAll().OfType<ActivityTemplate>().FirstOrDefaultAsync(at => at.ReferenceId == activityTemplate.ReferenceId);

                if (activityTemplateWithReferenceId != null)
                    return activityTemplateWithReferenceId;
            }

            activityTemplate.Id = await _abstractActivityRepository.InsertAndGetIdAsync(activityTemplate);

            return activityTemplate;
        }

        public void EditActivityTemplate(ActivityTemplate activityTemplate, string name, ICollection<ActivityTemplateReferenceTimeSlot> referenceTimeSlots, Location location, ICollection<Tag> tags, long editUserId)
        {
            activityTemplate.Edit(name, location, tags, editUserId);
            activityTemplate.Edit(referenceTimeSlots, editUserId);
        }

        public void EditDescriptions(ActivityTemplate activityTemplate, long[] descriptionIds, long editUserId)
        {
            var activityTemplateDescriptions = activityTemplate.Descriptions;

            foreach (var activityTemplateDescription in activityTemplateDescriptions)
                for (var i = 0; i < descriptionIds.Length; i++)
                    if (descriptionIds[i] == activityTemplateDescription.Id)
                        activityTemplateDescription.EditPriority(i, editUserId);
        }

        public async Task RemoveAsync(ActivityTemplate activityTemplate, long deleteUserId)
        {
            await activityTemplate.RemoveAsync(_abstractActivityRepository, deleteUserId);
        }
    }
}