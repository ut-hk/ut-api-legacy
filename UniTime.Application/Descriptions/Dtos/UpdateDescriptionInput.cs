using Abp.Application.Services.Dto;

namespace UniTime.Descriptions.Dtos
{
    public class UpdateDescriptionInput : EntityDto<long>
    {
        public string[] HTMLClasses { get; set; }
    }
}