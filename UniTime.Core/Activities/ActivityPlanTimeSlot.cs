using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.UI;
using UniTime.Interfaces;

namespace UniTime.Activities
{
    public class ActivityPlanTimeSlot : Entity<long>, ITimeSlot
    {
        protected ActivityPlanTimeSlot()
        {
        }

        [ForeignKey(nameof(ActivityPlanId))]
        public virtual ActivityPlan ActivityPlan { get; protected set; }

        [Required]
        public virtual Guid ActivityPlanId { get; protected set; }

        [ForeignKey(nameof(ActivityTemplateId))]
        public virtual ActivityTemplate ActivityTemplate { get; protected set; }

        [Required]
        public virtual Guid ActivityTemplateId { get; protected set; }

        public virtual DateTime? StartTime { get; protected set; }

        public virtual DateTime? EndTime { get; protected set; }

        public static ActivityPlanTimeSlot Create(ActivityPlan activityPlan, ActivityTemplate activityTemplate, DateTime? startTime, DateTime? endTime, long createUserId)
        {
            if (activityPlan.OwnerId != createUserId)
                throw new UserFriendlyException($"You are not allowed to add time slot in this activity paln with id = {createUserId}");

            return new ActivityPlanTimeSlot
            {
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id,
                ActivityTemplate = activityTemplate,
                ActivityTemplateId = activityTemplate.Id,
                StartTime = startTime,
                EndTime = endTime
            };
        }

        public void Edit(DateTime startTime, DateTime endTime, long editUserId)
        {
            if (ActivityPlan.OwnerId != editUserId)
                throw new UserFriendlyException($"You are not allowed to change this activity plan time slot with id = {editUserId}.");

            StartTime = startTime;
            EndTime = endTime;
        }
    }
}