using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Descriptions.Enums;

namespace UniTime.Descriptions.Dtos
{
    [AutoMapFrom(typeof(Description))]
    public class DescriptionDto : EntityDto<long>
    {
        public DescriptionType Type { get; set; }

        public string Content { get; set; }

        public string HTMLClasses { get; set; }

        public int Priority { get; set; }
    }
}