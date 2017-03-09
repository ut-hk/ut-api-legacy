using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using UniTime.Comments.Dtos;
using UniTime.Locations.Dtos;
using UniTime.Ratings.Dtos;
using UniTime.Tags.Dtos;

namespace UniTime.Activities.Dtos
{
    public class AbstractActivityDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public LocationDto Location { get; set; }

        public ICollection<TagDto> Tags { get; set; }

        public ICollection<RatingDto> Ratings { get; set; }

        public ICollection<CommentDto> Comments { get; set; }

        public long OwnerId { get; set; }
    }
}