using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using UniTime.Users.Enums;

namespace UniTime.Users.Dtos
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [JsonProperty(nameof(UserProfile.Gender))]
        public Gender ProfileGender { get; set; }

        [JsonProperty(nameof(UserProfile.Birthday))]
        public DateTime? ProfileBirthday { get; set; }

        [JsonProperty(nameof(UserProfile.CoverId))]
        public Guid? ProfileCoverId { get; set; }
    }
}