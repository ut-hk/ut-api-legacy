﻿using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Activities.Dtos;

namespace UniTime.Activities
{
    public interface IActivityTemplateAppService:IApplicationService
    {
        Task<GetActivityTemplatesOutput> GetActivityTemplates();

        Task<EntityDto<Guid>> CreateActivityTemplate(CreateActivityTemplateInput input);
    }
}