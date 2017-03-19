using System;
using System.Threading.Tasks;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.WebApi.Controllers;
using Microsoft.AspNet.Identity;
using UniTime.Users;
using UniTime.Users.Managers;

namespace UniTime.Api
{
    public class UniTimeApiControllerBase : AbpApiController
    {
        protected UniTimeApiControllerBase()
        {
            LocalizationSourceName = UniTimeConsts.LocalizationSourceName;
        }

        public UserManager UserManager { get; set; }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
                throw new ApplicationException("Please log in to do further action.");

            return user;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
                throw new UserFriendlyException("Invalid request.");
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}