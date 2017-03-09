using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Ratings.Enums;

namespace UniTime.Ratings.Dtos
{
    [AutoMapFrom(typeof(Rating))]
    public class RatingDto : EntityDto<Guid>
    {
        public RatingStatus RatingStatus { get; set; }

        public long OwnerId { get; set; }
    }
}