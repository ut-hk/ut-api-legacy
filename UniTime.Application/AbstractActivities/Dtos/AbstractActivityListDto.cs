using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using UniTime.Descriptions.Dtos;
using UniTime.Locations.Dtos;
using UniTime.Ratings.Enums;
using UniTime.Tags.Dtos;
using UniTime.Users.Dtos;

namespace UniTime.AbstractActivities.Dtos
{
    public class AbstractActivityListDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public LocationDto Location { get; set; }

        public ICollection<TagDto> Tags { get; set; }

        public UserListDto Owner { get; set; }

        public DescriptionDto CoverImageDescription { get; set; }

        public DescriptionDto CoverTextDescription { get; set; }

        public RatingStatus? MyRatingStatus { get; set; }

        public long Likes { get; set; }
    }
}