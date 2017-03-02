using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Activities.Dtos;
using UniTime.Ratings.Enums;
using UniTime.Users.Dtos;

namespace UniTime.Ratings.Dtos
{
    [AutoMapFrom(typeof(Rating))]
    public class RatingDto : EntityDto<Guid>
    {
        public RatingStatus RatingStatus { get; set; }

        public UserDto Owner { get; set; }
    }
}