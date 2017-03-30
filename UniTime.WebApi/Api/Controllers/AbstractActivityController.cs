using System;
using Abp.Domain.Repositories;
using Abp.WebApi.OData.Controllers;
using UniTime.Activities;

namespace UniTime.Api.Controllers
{
    public class AbstractActivityController : AbpODataEntityController<AbstractActivity, Guid>
    {
        public AbstractActivityController(IRepository<AbstractActivity, Guid> abstractActivityRepository)
            : base(abstractActivityRepository)
        {
        }
    }
}