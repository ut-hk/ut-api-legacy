using System.ComponentModel.DataAnnotations;

namespace UniTime.Users.Dtos
{
    public class TargetUserInput
    {
        [Required]
        public long TargetUserId { get; set; }
    }
}