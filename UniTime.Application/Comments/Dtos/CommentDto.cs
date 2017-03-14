using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Comments.Enums;

namespace UniTime.Comments.Dtos
{
    [AutoMapFrom(typeof(Comment))]
    public class CommentDto : EntityDto<Guid>
    {
        public string Content { get; set; }

        public CommentType Type { get; set; }

        public IReadOnlyList<ReplyDto> Replies { get; set; }
    }
}