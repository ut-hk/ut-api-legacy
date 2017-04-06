using System;
using Abp.Domain.Entities;
using UniTime.Interfaces;

namespace UniTime.Activities
{
    public class ActivityTemplateReferenceTimeSlot : Entity<long>, ITimeSlot
    {
        public virtual ActivityTemplate ActivityTemplate { get; set; }

        public virtual Guid ActivityTemplateId { get; set; }

        public virtual DateTime? StartTime { get; protected set; }

        public virtual DateTime? EndTime { get; protected set; }

        public static ActivityTemplateReferenceTimeSlot Create(DateTime? startTime, DateTime? endTime)
        {
            return new ActivityTemplateReferenceTimeSlot
            {
                StartTime = startTime,
                EndTime = endTime
            };
        }
    }
}