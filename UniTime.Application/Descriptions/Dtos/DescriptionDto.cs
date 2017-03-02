using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions.Dtos
{
    [AutoMapFrom(typeof(Description))]
    public class DescriptionDto : EntityDto<long>
    {
        public string Content { get; set; }

        public DescriptionType Type { get; set; }
    }
}