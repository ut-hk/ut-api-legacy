using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UniTime.Comments.Dtos
{
    [AutoMapFrom(typeof(Reply))]
    public class ReplyDto : EntityDto<long>
    {
        public string Content { get; set; }
    }
}