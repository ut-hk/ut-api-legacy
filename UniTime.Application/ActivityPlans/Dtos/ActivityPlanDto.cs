using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using UniTime.Activities;
using UniTime.Comments.Dtos;
using UniTime.Descriptions.Dtos;
using UniTime.Ratings.Dtos;
using UniTime.Tags.Dtos;
using UniTime.Users.Dtos;

namespace UniTime.ActivityPlans.Dtos
{
    [AutoMapFrom(typeof(ActivityPlan))]
    public class ActivityPlanDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<DescriptionDto> Descriptions { get; set; }

        public ICollection<TagDto> Tags { get; set; }

        public ICollection<ActivityPlanTimeSlotDto> TimeSlots { get; set; }

        public ICollection<CommentDto> Comments { get; set; }

        public ICollection<RatingDto> Ratings { get; set; }

        public UserListDto Owner { get; set; }
    }
}