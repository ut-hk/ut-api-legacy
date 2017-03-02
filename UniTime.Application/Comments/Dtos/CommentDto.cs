using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UniTime.Comments.Dtos
{
    [AutoMapFrom(typeof(Comment))]
    public class CommentDto : EntityDto<long>
    {
        public string Content { get; set; }

        public IReadOnlyList<ReplyDto> Replies { get; set; }
    }
}