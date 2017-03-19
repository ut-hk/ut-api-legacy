using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using UniTime.Users.Enums;

namespace UniTime.Users.Dtos
{
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        [JsonProperty(nameof(UserProfile.Gender))]
        public Gender? ProfileGender { get; set; }

        [JsonProperty(nameof(UserProfile.CoverId))]
        public Guid? ProfileCoverId { get; set; }
    }
}