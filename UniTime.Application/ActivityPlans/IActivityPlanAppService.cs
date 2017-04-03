using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.ActivityPlans.Dtos;

namespace UniTime.ActivityPlans
{
    public interface IActivityPlanAppService : IApplicationService
    {
        Task<GetActivityPlanOutput> GetActivityPlan(EntityDto<Guid> input);
        Task<GetActivityPlansOutput> GetActivityPlans(GetActivityPlansInput input);

        Task<EntityDto<Guid>> CreateActivityPlan(CreateActivityPlanInput input);

        Task UpdateActivityPlan(UpdateActivityPlanInput input);
    }
}