using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using UniTime.Comments.Dtos;
using UniTime.Descriptions.Dtos;
using UniTime.Locations.Dtos;
using UniTime.Ratings.Dtos;
using UniTime.Tags.Dtos;
using UniTime.Users.Dtos;

namespace UniTime.AbstractActivities.Dtos
{
    public class AbstractActivityDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public ICollection<DescriptionDto> Descriptions { get; set; }

        public LocationDto Location { get; set; }

        public ICollection<TagDto> Tags { get; set; }

        public ICollection<RatingDto> Ratings { get; set; }

        public ICollection<CommentDto> Comments { get; set; }

        public UserListDto Owner { get; set; }
    }
}