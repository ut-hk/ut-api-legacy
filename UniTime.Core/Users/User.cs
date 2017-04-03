using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNet.Identity;
using UniTime.Activities;
using UniTime.Analysis;
using UniTime.ChatRooms;
using UniTime.Comments;
using UniTime.Files;
using UniTime.Invitations;
using UniTime.Ratings;

namespace UniTime.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qweabpunitime";

        public virtual UserProfile Profile { get; internal set; }

        public virtual ICollection<AbstractActivity> AbstractActivities { get; protected set; }

        public virtual ICollection<ActivityParticipant> Participants { get; protected set; }

        public virtual ICollection<ActivityPlan> ActivityPlans { get; protected set; }

        public virtual ICollection<Guest> Guests { get; protected set; }

        public virtual ICollection<ChatRoom> ChatRooms { get; protected set; }

        public virtual ICollection<ChatRoomMessage> ChatRoomMessages { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        public virtual ICollection<Reply> Replies { get; protected set; }

        public virtual ICollection<File> Files { get; protected set; }

        public virtual ICollection<Rating> Ratings { get; protected set; }

        public virtual ICollection<Invitation> SentInvitations { get; protected set; }

        public virtual ICollection<Invitation> ReceivedInvitations { get; protected set; }

        public virtual ICollection<Track> Trackings { get; protected set; }

        public virtual ICollection<Track> TrackedBys { get; protected set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };
        }

        internal void EditUser(string name, string surname, string phoneNumber)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }

        public void EditPassword(string oldPassword, string password)
        {
            var passwordHasher = new PasswordHasher();

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(Password, oldPassword);

            switch (passwordVerificationResult)
            {
                case PasswordVerificationResult.Success:
                    Password = passwordHasher.HashPassword(password);
                    break;
                default:
                    throw new UserFriendlyException("Please validate your password.");
            }
        }
    }
}