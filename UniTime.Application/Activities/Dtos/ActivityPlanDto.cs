﻿using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Comments.Dtos;
using UniTime.Descriptions.Dtos;
using UniTime.Ratings.Dtos;
using UniTime.Users.Dtos;

namespace UniTime.Activities.Dtos
{
    [AutoMapFrom(typeof(ActivityPlan))]
    public class ActivityPlanDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public ICollection<DescriptionDto> Descriptions { get; set; }

        public ICollection<ActivityPlanTimeSlotDto> TimeSlots { get; set; }

        public ICollection<CommentDto> Comments { get; set; }

        public ICollection<RatingDto> Ratings { get; set; }

        public UserDto Owner { get; set; }
    }
}