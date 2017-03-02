using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Services;

namespace UniTime.Activities
{
    public interface IActivityAppService : IDomainService
    {
        Task<GetActivitiesOutput> GetActivities();

        Task<EntityDto<Guid>> CreateActivities(CreateActivitiesInput input);
    }

    public class CreateActivitiesInput
    {
    }

    public class GetActivitiesOutput
    {
    }
}