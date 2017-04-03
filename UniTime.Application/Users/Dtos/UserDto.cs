using System;
using Abp.AutoMapper;
using Newtonsoft.Json;

namespace UniTime.Users.Dtos
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : UserListDto
    {
        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [JsonProperty(nameof(UserProfile.Birthday))]
        public DateTime? ProfileBirthday { get; set; }

        [JsonProperty(nameof(UserProfile.CoverId))]
        public Guid? ProfileCoverId { get; set; }
    }
}