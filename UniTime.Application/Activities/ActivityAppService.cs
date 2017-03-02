using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace UniTime.Activities
{
    public class ActivityAppService : UniTimeAppServiceBase, IActivityAppService
    {
        public Task<GetActivitiesOutput> GetActivities()
        {
            throw new NotImplementedException();
        }

        public Task<EntityDto<Guid>> CreateActivities(CreateActivitiesInput input)
        {
            throw new NotImplementedException();
        }
    }
}