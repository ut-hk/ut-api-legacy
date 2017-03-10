﻿using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
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

        public virtual UserProfile Profile { get; set; }

        public virtual ICollection<AbstractActivity> AbstractActivities { get; protected set; }

        public virtual ICollection<ActivityParticipant> Participants { get; protected set; }

        public virtual ICollection<ActivityPlan> ActivityPlans { get; protected set; }

        public virtual ICollection<Guest> Guests { get; set; }

        public virtual ICollection<ChatRoom> ChatRooms { get; protected set; }

        public virtual ICollection<ChatRoomMessage> ChatRoomMessages { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        public virtual ICollection<Reply> Replies { get; protected set; }

        public virtual ICollection<File> Files { get; protected set; }

        public virtual ICollection<Rating> Ratings { get; protected set; }

        public virtual ICollection<Invitation> SentInvitations { get; protected set; }

        public virtual ICollection<Invitation> ReceivedInvitations { get; protected set; }

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
    }
}