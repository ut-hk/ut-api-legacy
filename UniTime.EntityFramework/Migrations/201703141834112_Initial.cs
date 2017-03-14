namespace UniTime.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbstractActivities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        LocationId = c.Guid(),
                        OwnerId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        ActivityTemplateId = c.Guid(),
                        ReferenceId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Category_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.AbstractActivities", t => t.ActivityTemplateId)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.LocationId)
                .Index(t => t.OwnerId)
                .Index(t => t.ActivityTemplateId)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AbstractActivityId = c.Guid(),
                        ActivityPlanId = c.Guid(),
                        OwnerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ImageId = c.Guid(),
                        Text = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Comment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InternalImageComment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TextComment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbstractActivities", t => t.AbstractActivityId)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlanId)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .ForeignKey("dbo.Files", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.AbstractActivityId)
                .Index(t => t.ActivityPlanId)
                .Index(t => t.OwnerId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.ActivityPlans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        OwnerId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Category_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.OwnerId)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Priority = c.Int(nullable: false),
                        ActivityPlanId = c.Guid(),
                        Path = c.String(),
                        ImageId = c.Guid(),
                        Text = c.String(),
                        YoutubeId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlanId)
                .ForeignKey("dbo.Files", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.ActivityPlanId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OriginalFileName = c.String(),
                        Description = c.String(),
                        OwnerId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.ChatRoomMessages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChatRoomId = c.Guid(nullable: false),
                        OwnerId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ImageId = c.Guid(),
                        Text = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoomId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .ForeignKey("dbo.Files", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.ChatRoomId)
                .Index(t => t.OwnerId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.ChatRooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        OwnerId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.RouteHistories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RouteName = c.String(nullable: false),
                        Parameters = c.String(nullable: false),
                        Referer = c.String(),
                        GuestId = c.Guid(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .Index(t => t.GuestId);
            
            CreateTable(
                "dbo.ActivityParticipants",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ActivityId = c.Guid(nullable: false),
                        OwnerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbstractActivities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.ActivityId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Coordinate = c.Geography(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActivityPlanTimeSlots",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ActivityPlanId = c.Guid(nullable: false),
                        ActivityTemplateId = c.Guid(nullable: false),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlanId, cascadeDelete: true)
                .ForeignKey("dbo.AbstractActivities", t => t.ActivityTemplateId, cascadeDelete: true)
                .Index(t => t.ActivityPlanId)
                .Index(t => t.ActivityTemplateId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RatingStatus = c.Int(nullable: false),
                        AbstractActivityId = c.Guid(),
                        ActivityPlanId = c.Guid(),
                        OwnerId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbstractActivities", t => t.AbstractActivityId)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlanId)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.AbstractActivityId)
                .Index(t => t.ActivityPlanId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.ActivityTemplateReferenceTimeSlots",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        ActivityTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbstractActivities", t => t.ActivityTemplate_Id)
                .Index(t => t.ActivityTemplate_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 256),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Text);
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        Status = c.Int(nullable: false),
                        InviteeId = c.Long(nullable: false),
                        OwnerId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ActivityId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbstractActivities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.InviteeId)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.InviteeId)
                .Index(t => t.OwnerId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(),
                        CoverId = c.Guid(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.CoverId)
                .ForeignKey("dbo.AbpUsers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CoverId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Content = c.String(),
                        CommentId = c.Guid(nullable: false),
                        OwnerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Reply_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.CommentId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FromId = c.Long(nullable: false),
                        ToId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.FromId)
                .ForeignKey("dbo.AbpUsers", t => t.ToId)
                .Index(t => t.FromId)
                .Index(t => t.ToId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagAbstractActivities",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        AbstractActivity_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.AbstractActivity_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.AbstractActivities", t => t.AbstractActivity_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.AbstractActivity_Id);
            
            CreateTable(
                "dbo.TagActivityPlans",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        ActivityPlan_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.ActivityPlan_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlan_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.ActivityPlan_Id);
            
            CreateTable(
                "dbo.AbstractActivityImages",
                c => new
                    {
                        AbstractActivity_Id = c.Guid(nullable: false),
                        Image_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AbstractActivity_Id, t.Image_Id })
                .ForeignKey("dbo.AbstractActivities", t => t.AbstractActivity_Id, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.Image_Id, cascadeDelete: true)
                .Index(t => t.AbstractActivity_Id)
                .Index(t => t.Image_Id);
            
            AddColumn("dbo.AbpUsers", "ChatRoom_Id", c => c.Guid());
            CreateIndex("dbo.AbpUsers", "ChatRoom_Id");
            AddForeignKey("dbo.AbpUsers", "ChatRoom_Id", "dbo.ChatRooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbstractActivities", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ActivityPlans", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.AbstractActivities", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbstractActivityImages", "Image_Id", "dbo.Files");
            DropForeignKey("dbo.AbstractActivityImages", "AbstractActivity_Id", "dbo.AbstractActivities");
            DropForeignKey("dbo.Comments", "ImageId", "dbo.Files");
            DropForeignKey("dbo.Comments", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.ActivityPlans", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Descriptions", "ImageId", "dbo.Files");
            DropForeignKey("dbo.Tracks", "ToId", "dbo.AbpUsers");
            DropForeignKey("dbo.Tracks", "FromId", "dbo.AbpUsers");
            DropForeignKey("dbo.Replies", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Invitations", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Invitations", "InviteeId", "dbo.AbpUsers");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.AbpUsers");
            DropForeignKey("dbo.UserProfiles", "CoverId", "dbo.Files");
            DropForeignKey("dbo.ActivityParticipants", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.ActivityParticipants", "ActivityId", "dbo.AbstractActivities");
            DropForeignKey("dbo.Invitations", "ActivityId", "dbo.AbstractActivities");
            DropForeignKey("dbo.AbstractActivities", "ActivityTemplateId", "dbo.AbstractActivities");
            DropForeignKey("dbo.TagActivityPlans", "ActivityPlan_Id", "dbo.ActivityPlans");
            DropForeignKey("dbo.TagActivityPlans", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagAbstractActivities", "AbstractActivity_Id", "dbo.AbstractActivities");
            DropForeignKey("dbo.TagAbstractActivities", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplate_Id", "dbo.AbstractActivities");
            DropForeignKey("dbo.Ratings", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Ratings", "ActivityPlanId", "dbo.ActivityPlans");
            DropForeignKey("dbo.Ratings", "AbstractActivityId", "dbo.AbstractActivities");
            DropForeignKey("dbo.ActivityPlanTimeSlots", "ActivityTemplateId", "dbo.AbstractActivities");
            DropForeignKey("dbo.ActivityPlanTimeSlots", "ActivityPlanId", "dbo.ActivityPlans");
            DropForeignKey("dbo.AbstractActivities", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.RouteHistories", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Files", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.ChatRoomMessages", "ImageId", "dbo.Files");
            DropForeignKey("dbo.ChatRoomMessages", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "ChatRoom_Id", "dbo.ChatRooms");
            DropForeignKey("dbo.ChatRooms", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.ChatRoomMessages", "ChatRoomId", "dbo.ChatRooms");
            DropForeignKey("dbo.Descriptions", "ActivityPlanId", "dbo.ActivityPlans");
            DropForeignKey("dbo.Comments", "ActivityPlanId", "dbo.ActivityPlans");
            DropForeignKey("dbo.Comments", "AbstractActivityId", "dbo.AbstractActivities");
            DropIndex("dbo.AbstractActivityImages", new[] { "Image_Id" });
            DropIndex("dbo.AbstractActivityImages", new[] { "AbstractActivity_Id" });
            DropIndex("dbo.TagActivityPlans", new[] { "ActivityPlan_Id" });
            DropIndex("dbo.TagActivityPlans", new[] { "Tag_Id" });
            DropIndex("dbo.TagAbstractActivities", new[] { "AbstractActivity_Id" });
            DropIndex("dbo.TagAbstractActivities", new[] { "Tag_Id" });
            DropIndex("dbo.Tracks", new[] { "ToId" });
            DropIndex("dbo.Tracks", new[] { "FromId" });
            DropIndex("dbo.Replies", new[] { "OwnerId" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.UserProfiles", new[] { "CoverId" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Invitations", new[] { "ActivityId" });
            DropIndex("dbo.Invitations", new[] { "OwnerId" });
            DropIndex("dbo.Invitations", new[] { "InviteeId" });
            DropIndex("dbo.Tags", new[] { "Text" });
            DropIndex("dbo.ActivityTemplateReferenceTimeSlots", new[] { "ActivityTemplate_Id" });
            DropIndex("dbo.Ratings", new[] { "OwnerId" });
            DropIndex("dbo.Ratings", new[] { "ActivityPlanId" });
            DropIndex("dbo.Ratings", new[] { "AbstractActivityId" });
            DropIndex("dbo.ActivityPlanTimeSlots", new[] { "ActivityTemplateId" });
            DropIndex("dbo.ActivityPlanTimeSlots", new[] { "ActivityPlanId" });
            DropIndex("dbo.ActivityParticipants", new[] { "OwnerId" });
            DropIndex("dbo.ActivityParticipants", new[] { "ActivityId" });
            DropIndex("dbo.RouteHistories", new[] { "GuestId" });
            DropIndex("dbo.Guests", new[] { "OwnerId" });
            DropIndex("dbo.ChatRooms", new[] { "OwnerId" });
            DropIndex("dbo.ChatRoomMessages", new[] { "ImageId" });
            DropIndex("dbo.ChatRoomMessages", new[] { "OwnerId" });
            DropIndex("dbo.ChatRoomMessages", new[] { "ChatRoomId" });
            DropIndex("dbo.AbpUsers", new[] { "ChatRoom_Id" });
            DropIndex("dbo.Files", new[] { "OwnerId" });
            DropIndex("dbo.Descriptions", new[] { "ImageId" });
            DropIndex("dbo.Descriptions", new[] { "ActivityPlanId" });
            DropIndex("dbo.ActivityPlans", new[] { "Category_Id" });
            DropIndex("dbo.ActivityPlans", new[] { "OwnerId" });
            DropIndex("dbo.Comments", new[] { "ImageId" });
            DropIndex("dbo.Comments", new[] { "OwnerId" });
            DropIndex("dbo.Comments", new[] { "ActivityPlanId" });
            DropIndex("dbo.Comments", new[] { "AbstractActivityId" });
            DropIndex("dbo.AbstractActivities", new[] { "Category_Id" });
            DropIndex("dbo.AbstractActivities", new[] { "ActivityTemplateId" });
            DropIndex("dbo.AbstractActivities", new[] { "OwnerId" });
            DropIndex("dbo.AbstractActivities", new[] { "LocationId" });
            DropColumn("dbo.AbpUsers", "ChatRoom_Id");
            DropTable("dbo.AbstractActivityImages");
            DropTable("dbo.TagActivityPlans");
            DropTable("dbo.TagAbstractActivities");
            DropTable("dbo.Categories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Tracks");
            DropTable("dbo.Replies",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Reply_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Invitations");
            DropTable("dbo.Tags",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ActivityTemplateReferenceTimeSlots");
            DropTable("dbo.Ratings");
            DropTable("dbo.ActivityPlanTimeSlots");
            DropTable("dbo.Locations");
            DropTable("dbo.ActivityParticipants");
            DropTable("dbo.RouteHistories");
            DropTable("dbo.Guests");
            DropTable("dbo.ChatRooms");
            DropTable("dbo.ChatRoomMessages");
            DropTable("dbo.Files");
            DropTable("dbo.Descriptions");
            DropTable("dbo.ActivityPlans");
            DropTable("dbo.Comments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Comment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InternalImageComment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TextComment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbstractActivities");
        }
    }
}
