using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using UniTime.Api.Dtos;
using UniTime.Authorization.Managers;
using UniTime.Authorization.Roles;
using UniTime.MultiTenancy;
using UniTime.MultiTenancy.Managers;
using UniTime.Users;
using UniTime.Users.Managers;

namespace UniTime.Api.Controllers
{
    public class AccountController : UniTimeApiControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly RoleManager _roleManager;
        private readonly TenantManager _tenantManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserManager _userManager;

        static AccountController()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        }

        public AccountController(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            LogInManager logInManager,
            IMultiTenancyConfig multiTenancyConfig)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
            _logInManager = logInManager;
            _multiTenancyConfig = multiTenancyConfig;
        }

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; }

        [Route("Account/LogIn")]
        [HttpPost]
        public async Task<AjaxResponse> LogIn(LogInInput input)
        {
            CheckModelState();

            var logInResult = await GetLogInResultAsync(
                input.UsernameOrEmailAddress,
                input.Password,
                input.TenancyName
            );

            var ticket = new AuthenticationTicket(logInResult.Identity, new AuthenticationProperties());

            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromDays(7));

            return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLogInResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), "Your email address is not confirmed. You can not login"); // TODO: localize message
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        [Route("Account/SignUp")]
        [HttpPost]
        [UnitOfWork]
        public virtual async Task<AjaxResponse> SignUp(SignUpInput input)
        {
            CheckModelState();

            // Get tenancy name and tenant
            if (!_multiTenancyConfig.IsEnabled)
                input.TenancyName = Tenant.DefaultTenantName;
            else if (input.TenancyName.IsNullOrEmpty())
                throw new UserFriendlyException(L("TenantNameCanNotBeEmpty"));

            var tenant = await GetActiveTenantAsync(input.TenancyName);

            // Create user
            var user = new User
            {
                TenantId = tenant.Id,
                Name = input.Name,
                Surname = input.Surname,
                EmailAddress = input.EmailAddress,
                IsActive = true
            };


            if (input.UserName.IsNullOrEmpty() || input.Password.IsNullOrEmpty())
                throw new UserFriendlyException(L("FormIsNotValidMessage"));


            user.UserName = input.UserName;
            user.Password = new PasswordHasher().HashPassword(input.Password);

            // Switch to the tenant
            _unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant); //TODO: Needed?
            _unitOfWorkManager.Current.SetTenantId(tenant.Id);

            // Add default roles
            user.Roles = new List<UserRole>();
            foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                user.Roles.Add(new UserRole {RoleId = defaultRole.Id});

            // Save user
            CheckErrors(await _userManager.CreateAsync(user));
            await _unitOfWorkManager.Current.SaveChangesAsync();

            // If can not login, show a register result page
            return await LogIn(new LogInInput
            {
                UsernameOrEmailAddress = input.UserName,
                Password = input.Password
            });
        }

        private async Task<Tenant> GetActiveTenantAsync(string tenancyName)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            if (tenant == null)
                throw new UserFriendlyException(L("ThereIsNoTenantDefinedWithName{0}", tenancyName));

            if (!tenant.IsActive)
                throw new UserFriendlyException(L("TenantIsNotActive", tenancyName));

            return tenant;
        }
    }

    public class SignUpInput : IValidatableObject
    {
        /// <summary>
        ///     Not required for single-tenant applications.
        /// </summary>
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (!UserName.Equals(EmailAddress) && emailRegex.IsMatch(UserName))
                yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
        }
    }
}