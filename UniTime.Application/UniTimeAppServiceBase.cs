using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using UniTime.MultiTenancy;
using UniTime.MultiTenancy.Managers;
using UniTime.Users;
using UniTime.Users.Managers;

namespace UniTime
{
    /// <summary>
    ///     Derive your application services from this class.
    /// </summary>
    public abstract class UniTimeAppServiceBase : ApplicationService
    {
        protected UniTimeAppServiceBase()
        {
            LocalizationSourceName = UniTimeConsts.LocalizationSourceName;
        }

        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
                throw new ApplicationException("Please log in again.");

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}