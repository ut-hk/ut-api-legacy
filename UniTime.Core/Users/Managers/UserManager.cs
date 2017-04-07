using System;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using UniTime.Authorization.Roles;
using UniTime.Files;
using UniTime.Users.Enums;

namespace UniTime.Users.Managers
{
    public class UserManager : AbpUserManager<Role, User>
    {
        public UserManager(
            UserStore userStore,
            RoleManager roleManager,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ILocalizationManager localizationManager,
            ISettingManager settingManager,
            IdentityEmailMessageService emailService,
            IUserTokenProviderAccessor userTokenProviderAccessor)
            : base(
                userStore,
                roleManager,
                permissionManager,
                unitOfWorkManager,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                localizationManager,
                emailService,
                settingManager,
                userTokenProviderAccessor)
        {
        }

        public void EditUser(User user, string name, string surname, string phoneNumber, Gender? gender, DateTime? birthday, Image icon, Image cover)
        {
            user.EditUser(name, surname, phoneNumber);

            if (user.Profile == null)
                user.Profile = UserProfile.Create(user, gender, birthday, icon, cover);
            else
                user.Profile.EditUserProfile(gender, birthday, icon, cover);
        }

        public void EditPassword(User user, string oldPassword, string newPassword)
        {
            user.EditPassword(oldPassword, newPassword);
        }
    }
}