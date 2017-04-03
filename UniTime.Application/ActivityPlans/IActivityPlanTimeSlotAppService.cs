using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.ActivityPlans.Dtos;

namespace UniTime.ActivityPlans
{
    public interface IActivityPlanTimeSlotAppService : IApplicationService
    {
        Task<EntityDto<long>> CreateActivityPlanTimeSlot(CreateActivityPlanTimeSlotInput input);

        Task UpdateActivityPlanTimeSlot(UpdateActivityPlanTimeSlotInput input);

        Task RemoveActivityPlanTimeSlot(EntityDto<long> input);
    }
}