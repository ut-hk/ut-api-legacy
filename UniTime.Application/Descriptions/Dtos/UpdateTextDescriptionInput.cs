using Abp.Application.Services.Dto;

namespace UniTime.Descriptions.Dtos
{
    public class UpdateTextDescriptionInput : EntityDto<long>
    {
        public string Text { get; set; }
    }
}