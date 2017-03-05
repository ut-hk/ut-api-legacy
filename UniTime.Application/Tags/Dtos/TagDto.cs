using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UniTime.Tags.Dtos
{
    [AutoMapFrom(typeof(Tag))]
    public class TagDto : EntityDto<long>
    {
        public string Text { get; set; }
    }
}