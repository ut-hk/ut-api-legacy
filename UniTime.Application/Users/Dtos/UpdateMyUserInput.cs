using System;
using System.ComponentModel.DataAnnotations;
using UniTime.Users.Enums;

namespace UniTime.Users.Dtos
{
    public class UpdateMyUserInput
    {
        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public Guid? CoverId { get; set; }
    }
}