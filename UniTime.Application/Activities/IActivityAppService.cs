using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Activities.Dtos;

namespace UniTime.Activities
{
    public interface IActivityAppService : IApplicationService
    {
        Task<GetMyActivitiesOutput> GetMyActivities();

        Task<EntityDto<Guid>> CreateActivity(CreateActivityInput input);

        Task UpdateActivity(UpdateAbstractActivityInput input);
    }
}