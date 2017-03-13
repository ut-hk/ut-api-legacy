using System;
using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using UniTime.Activities;
using UniTime.Analysis;
using UniTime.Authorization.Roles;
using UniTime.Categories;
using UniTime.ChatRooms;
using UniTime.Comments;
using UniTime.Descriptions;
using UniTime.Files;
using UniTime.Invitations;
using UniTime.Locations;
using UniTime.MultiTenancy;
using UniTime.Ratings;
using UniTime.Tags;
using UniTime.Users;

namespace UniTime.EntityFramework
{
    public class UniTimeDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* NOTE: 
     *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
     *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
     *   pass connection string name to base classes. ABP works either way.
     */

        public UniTimeDbContext()
            : base("Default")
        {
        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in UniTimeDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of UniTimeDbContext since ABP automatically handles it.
         */

        public UniTimeDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        // This constructor is used in tests
        public UniTimeDbContext(DbConnection connection)
            : base(connection, true)
        {
        }

        public IDbSet<AbstractActivity> AbstractActivities { get; set; }

        public IDbSet<ActivityParticipant> ActivityParticipants { get; set; }

        public IDbSet<ActivityPlan> ActivityPlans { get; set; }

        public IDbSet<ActivityPlanTimeSlot> ActivityPlanTimeSlots { get; set; }

        public IDbSet<ActivityTemplateReferenceTimeSlot> ActivityTemplateReferenceTimeSlots { get; set; }

        public IDbSet<Guest> Guests { get; set; }

        public IDbSet<RouteHistory> RouteHistories { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<ChatRoom> ChatRooms { get; set; }

        public IDbSet<ChatRoomMessage> ChatRoomMessages { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Reply> Replies { get; set; }

        public IDbSet<Description> Descriptions { get; set; }

        public IDbSet<File> Files { get; set; }

        public IDbSet<Invitation> Invitations { get; set; }

        public IDbSet<Location> Locations { get; set; }

        public IDbSet<Rating> Ratings { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<Track> Tracks { get; set; }

        public IDbSet<UserProfile> UserProfiles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (Database.Connection.GetType().Name.StartsWith("Effort", StringComparison.Ordinal))
                modelBuilder.Entity<Location>().Ignore(location => location.Coordinate);

            modelBuilder.Entity<ActivityParticipant>()
                .HasRequired(activityParticipant => activityParticipant.Owner)
                .WithMany(user => user.Participants);

            modelBuilder.Entity<Invitation>()
                .HasRequired(invitation => invitation.Owner)
                .WithMany(user => user.SentInvitations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invitation>()
                .HasRequired(invitation => invitation.Invitee)
                .WithMany(user => user.ReceivedInvitations)
                .WillCascadeOnDelete(false);

            ApplyOwnerRelationships(modelBuilder);
        }

        private static void ApplyOwnerRelationships(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbstractActivity>()
                .HasRequired(abstractActivity => abstractActivity.Owner)
                .WithMany(user => user.AbstractActivities)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ActivityParticipant>()
                .HasRequired(activityParticipant => activityParticipant.Owner)
                .WithMany(user => user.Participants)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ActivityPlan>()
                .HasRequired(activityPlan => activityPlan.Owner)
                .WithMany(user => user.ActivityPlans)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatRoom>()
                .HasRequired(chatRoom => chatRoom.Owner)
                .WithMany(user => user.ChatRooms)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatRoomMessage>()
                .HasRequired(chatRoomMessage => chatRoomMessage.Owner)
                .WithMany(user => user.ChatRoomMessages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .HasRequired(comment => comment.Owner)
                .WithMany(user => user.Comments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reply>()
                .HasRequired(reply => reply.Owner)
                .WithMany(user => user.Replies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<File>()
                .HasRequired(file => file.Owner)
                .WithMany(user => user.Files)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .HasRequired(rating => rating.Owner)
                .WithMany(user => user.Ratings)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Track>()
                .HasRequired(track => track.From)
                .WithMany(user => user.Trackings)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Track>()
                .HasRequired(track => track.To)
                .WithMany(user => user.TrackedBys)
                .WillCascadeOnDelete(false);
        }
    }
}