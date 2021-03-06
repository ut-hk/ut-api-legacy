﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using UniTime.Locations;
using UniTime.Tags;

namespace UniTime.Activities.Managers
{
    public interface IActivityTemplateManager : IDomainService
    {
        string DoesNotExistMessage { get; }

        Task<ActivityTemplate> GetAsync(Guid id);

        Task<ActivityTemplate> CreateAsync(ActivityTemplate activityTemplate);

        void EditActivityTemplate(ActivityTemplate activityTemplate, string name, ICollection<ActivityTemplateReferenceTimeSlot> referenceTimeSlots, Location location, ICollection<Tag> tags, long editUserId);
        void EditDescriptions(ActivityTemplate activityTemplate, long[] descriptionIds, long editUserId);

        Task RemoveAsync(ActivityTemplate activityTemplate, long deleteUserId);
    }
}