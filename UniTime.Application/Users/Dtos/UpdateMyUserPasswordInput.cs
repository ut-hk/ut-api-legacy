using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace UniTime.Users.Dtos
{
    public class UpdateMyUserPasswordInput
    {
        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string OldPassword { get; set; }

        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string NewPassword { get; set; }
    }
}