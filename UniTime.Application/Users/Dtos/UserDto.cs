﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UniTime.Users.Dtos
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
    }
}