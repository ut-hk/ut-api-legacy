using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization.Users;
using Abp.Configuration.Startup;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using UniTime.Authorization.Managers;
using UniTime.MultiTenancy;
using UniTime.Users;
using UniTime.Users.Managers;
using UniTime.Web.Models.Account;

namespace UniTime.Web.Controllers
{
    public class AccountController : UniTimeControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly UserManager _userManager;

        public AccountController(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            LogInManager logInManager)
        {
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _logInManager = logInManager;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        #region LogIn / LogOut

        public ActionResult LogIn(string returnUrl = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
                returnUrl = Request.ApplicationPath;

            return View(
                new LogInFormViewModel
                {
                    ReturnUrl = returnUrl,
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                });
        }

        [HttpPost]
        public async Task<JsonResult> LogIn(LogInViewModel logInModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();

            var loginResult = await GetLogInResultAsync(
                logInModel.UsernameOrEmailAddress,
                logInModel.Password,
                logInModel.TenancyName
            );

            await SignInAsync(loginResult.User, loginResult.Identity, logInModel.RememberMe);

            if (string.IsNullOrWhiteSpace(returnUrl))
                returnUrl = Request.ApplicationPath;

            if (!string.IsNullOrWhiteSpace(returnUrlHash))
                returnUrl = returnUrl + returnUrlHash;

            return Json(new AjaxResponse {TargetUrl = returnUrl});
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

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = rememberMe}, identity);
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
                    return new UserFriendlyException(L("LoginFailed"), "Your email address is not confirmed. You can not login"); //TODO: localize message
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("LogIn");
        }

        #endregion
    }
}