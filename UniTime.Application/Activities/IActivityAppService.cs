using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Activities.Dtos;

namespace UniTime.Activities
{
    public interface IActivityAppService : IApplicationService
    {
        Task<GetActivityOutput> GetActivity(EntityDto<Guid> input);

        Task<GetActivitiesOutput> GetActivities(GetActivitiesInput input);

        /// <summary>
        ///     My Activities are the owned activities and the participated activities.
        /// </summary>
        /// <returns></returns>
        Task<GetMyActivitiesOutput> GetMyActivities();

        Task<EntityDto<Guid>> CreateActivity(CreateActivityInput input);

        Task UpdateActivity(UpdateActivityInput input);
    }
}