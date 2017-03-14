using System;

namespace UniTime.Descriptions.Dtos
{
    public class CreateInternalImageDescriptionInput : CreateDescriptionInput
    {
        public Guid ImageId { get; set; }
    }
}