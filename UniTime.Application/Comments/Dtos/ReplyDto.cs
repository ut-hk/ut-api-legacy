using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using UniTime.Users.Dtos;

namespace UniTime.Comments.Dtos
{
    [AutoMapFrom(typeof(Reply))]
    public class ReplyDto : EntityDto<long>, IHasCreationTime
    {
        public string Content { get; set; }

        public UserListDto Owner { get; set; }

        public DateTime CreationTime { get; set; }
    }
}