using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Activities.Dtos;

namespace UniTime.Activities
{
    public interface IActivityPlanAppService : IApplicationService
    {
        Task<GetActivityPlansOutput> GetActivityPlans();

        Task<EntityDto<Guid>> CreateActivityPlan(CreateActivityPlanInput input);
    }
}