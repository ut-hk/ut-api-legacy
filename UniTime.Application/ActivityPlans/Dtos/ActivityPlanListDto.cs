using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Activities;
using UniTime.Descriptions.Dtos;
using UniTime.Ratings.Enums;
using UniTime.Tags.Dtos;
using UniTime.Users.Dtos;

namespace UniTime.ActivityPlans.Dtos
{
    [AutoMapFrom(typeof(ActivityPlan))]
    public class ActivityPlanListDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public ICollection<TagDto> Tags { get; set; }

        public UserListDto Owner { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DescriptionDto CoverImageDescription { get; set; }

        public DescriptionDto CoverTextDescription { get; set; }

        public RatingStatus? MyRatingStatus { get; set; }

        public long Likes { get; set; }
    }
}