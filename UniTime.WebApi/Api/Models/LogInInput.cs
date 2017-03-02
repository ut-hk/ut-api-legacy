using System.ComponentModel.DataAnnotations;

namespace UniTime.Api.Models
{
    public class LogInInput
    {
        public string TenancyName { get; set; }

        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}