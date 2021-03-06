﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<GetUserOutput> GetUser(EntityDto<long> input);
        Task<GetMyUserOutput> GetMyUser();

        Task UpdateMyUserPassword(UpdateMyUserPasswordInput input);
        Task UpdateMyUser(UpdateMyUserInput input);
    }
}