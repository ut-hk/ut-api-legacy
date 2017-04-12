using System;
using System.ComponentModel.DataAnnotations;

namespace UniTime.AbstractActivities.Dtos
{
    public class CreateAbstractActivityInput
    {
        public string Name { get; set; }

        public Guid? LocationId { get; set; }

        [Required]
        public string[] TagTexts { get; set; } = { };
    }
}