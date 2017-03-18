using Abp.Application.Services.Dto;

namespace UniTime.Descriptions.Dtos
{
    public class UpdateTextDescriptionInput : UpdateDescriptionInput
    {
        public string Text { get; set; }
    }
}