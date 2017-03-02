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
                        LocationId = c.Guid(nullable: false),
                        OwnerId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ReferenceStarTime = c.DateTime(),
                        ReferenceEndTime = c.DateTime(),
                        StarTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        ActivityTemplateId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.AbstractActivities", t => t.ActivityTemplateId)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.LocationId)
                .Index(t => t.OwnerId)
                .Index(t => t.ActivityTemplateId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Content = c.String(),
                        OwnerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        AbstractActivityId = c.Guid(),
                        ActivityPlanId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AbstractActivityComment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ActivityPlanComment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Comment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbstractActivities", t => t.AbstractActivityId, cascadeDelete: true)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlanId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.OwnerId)
                .Index(t => t.AbstractActivityId)
                .Index(t => t.ActivityPlanId);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Content = c.String(),
                        CommentId = c.Long(nullable: false),
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
                "dbo.Descriptions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        ActivityPlanId = c.Guid(),
                        Path = c.String(),
                        Text = c.String(),
                        YoutubeId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlanId, cascadeDelete: true)
                .Index(t => t.ActivityPlanId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RatingStatus = c.Int(nullable: false),
                        OwnerId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ActivityPlanId = c.Guid(),
                        AbstractActivityId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlanId, cascadeDelete: true)
                .ForeignKey("dbo.AbstractActivities", t => t.AbstractActivityId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.OwnerId)
                .Index(t => t.ActivityPlanId)
                .Index(t => t.AbstractActivityId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AbstractActivityTag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ActivityPlanTag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Tag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
                "dbo.Locations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.ChatRoomMessages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
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
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.ChatRoomId)
                .Index(t => t.OwnerId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.ChatRooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        OwnerId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        CoverId = c.Guid(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.CoverId)
                .ForeignKey("dbo.AbpUsers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CoverId);
            
            CreateTable(
                "dbo.ActivityPlanTagActivityPlans",
                c => new
                    {
                        ActivityPlanTag_Id = c.Long(nullable: false),
                        ActivityPlan_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActivityPlanTag_Id, t.ActivityPlan_Id })
                .ForeignKey("dbo.Tags", t => t.ActivityPlanTag_Id, cascadeDelete: true)
                .ForeignKey("dbo.ActivityPlans", t => t.ActivityPlan_Id, cascadeDelete: true)
                .Index(t => t.ActivityPlanTag_Id)
                .Index(t => t.ActivityPlan_Id);
            
            CreateTable(
                "dbo.AbstractActivityTagAbstractActivities",
                c => new
                    {
                        AbstractActivityTag_Id = c.Long(nullable: false),
                        AbstractActivity_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AbstractActivityTag_Id, t.AbstractActivity_Id })
                .ForeignKey("dbo.Tags", t => t.AbstractActivityTag_Id, cascadeDelete: true)
                .ForeignKey("dbo.AbstractActivities", t => t.AbstractActivity_Id, cascadeDelete: true)
                .Index(t => t.AbstractActivityTag_Id)
                .Index(t => t.AbstractActivity_Id);
            
            AddColumn("dbo.AbpUsers", "ChatRoom_Id", c => c.Guid());
            CreateIndex("dbo.AbpUsers", "ChatRoom_Id");
            AddForeignKey("dbo.AbpUsers", "ChatRoom_Id", "dbo.ChatRooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbstractActivities", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Invitations", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Invitations", "InviteeId", "dbo.AbpUsers");
            DropForeignKey("dbo.Ratings", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.AbpUsers");
            DropForeignKey("dbo.UserProfiles", "CoverId", "dbo.Images");
            DropForeignKey("dbo.ChatRoomMessages", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Images", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.ChatRoomMessages", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "ChatRoom_Id", "dbo.ChatRooms");
            DropForeignKey("dbo.ChatRoomMessages", "ChatRoomId", "dbo.ChatRooms");
            DropForeignKey("dbo.ActivityPlanTimeSlots", "ActivityTemplateId", "dbo.AbstractActivities");
            DropForeignKey("dbo.ActivityParticipants", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.ActivityParticipants", "ActivityId", "dbo.AbstractActivities");
            DropForeignKey("dbo.Invitations", "ActivityId", "dbo.AbstractActivities");
            DropForeignKey("dbo.AbstractActivities", "ActivityTemplateId", "dbo.AbstractActivities");
            DropForeignKey("dbo.AbstractActivityTagAbstractActivities", "AbstractActivity_Id", "dbo.AbstractActivities");
            DropForeignKey("dbo.AbstractActivityTagAbstractActivities", "AbstractActivityTag_Id", "dbo.Tags");
            DropForeignKey("dbo.Ratings", "AbstractActivityId", "dbo.AbstractActivities");
            DropForeignKey("dbo.AbstractActivities", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.ActivityPlanTimeSlots", "ActivityPlanId", "dbo.ActivityPlans");
            DropForeignKey("dbo.ActivityPlanTagActivityPlans", "ActivityPlan_Id", "dbo.ActivityPlans");
            DropForeignKey("dbo.ActivityPlanTagActivityPlans", "ActivityPlanTag_Id", "dbo.Tags");
            DropForeignKey("dbo.Ratings", "ActivityPlanId", "dbo.ActivityPlans");
            DropForeignKey("dbo.ActivityPlans", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Descriptions", "ActivityPlanId", "dbo.ActivityPlans");
            DropForeignKey("dbo.Replies", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.Comments", "ActivityPlanId", "dbo.ActivityPlans");
            DropForeignKey("dbo.Comments", "AbstractActivityId", "dbo.AbstractActivities");
            DropIndex("dbo.AbstractActivityTagAbstractActivities", new[] { "AbstractActivity_Id" });
            DropIndex("dbo.AbstractActivityTagAbstractActivities", new[] { "AbstractActivityTag_Id" });
            DropIndex("dbo.ActivityPlanTagActivityPlans", new[] { "ActivityPlan_Id" });
            DropIndex("dbo.ActivityPlanTagActivityPlans", new[] { "ActivityPlanTag_Id" });
            DropIndex("dbo.UserProfiles", new[] { "CoverId" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Images", new[] { "OwnerId" });
            DropIndex("dbo.ChatRoomMessages", new[] { "ImageId" });
            DropIndex("dbo.ChatRoomMessages", new[] { "OwnerId" });
            DropIndex("dbo.ChatRoomMessages", new[] { "ChatRoomId" });
            DropIndex("dbo.ActivityParticipants", new[] { "OwnerId" });
            DropIndex("dbo.ActivityParticipants", new[] { "ActivityId" });
            DropIndex("dbo.Invitations", new[] { "ActivityId" });
            DropIndex("dbo.Invitations", new[] { "OwnerId" });
            DropIndex("dbo.Invitations", new[] { "InviteeId" });
            DropIndex("dbo.ActivityPlanTimeSlots", new[] { "ActivityTemplateId" });
            DropIndex("dbo.ActivityPlanTimeSlots", new[] { "ActivityPlanId" });
            DropIndex("dbo.Ratings", new[] { "AbstractActivityId" });
            DropIndex("dbo.Ratings", new[] { "ActivityPlanId" });
            DropIndex("dbo.Ratings", new[] { "OwnerId" });
            DropIndex("dbo.Descriptions", new[] { "ActivityPlanId" });
            DropIndex("dbo.Replies", new[] { "OwnerId" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.ActivityPlans", new[] { "OwnerId" });
            DropIndex("dbo.AbpUsers", new[] { "ChatRoom_Id" });
            DropIndex("dbo.Comments", new[] { "ActivityPlanId" });
            DropIndex("dbo.Comments", new[] { "AbstractActivityId" });
            DropIndex("dbo.Comments", new[] { "OwnerId" });
            DropIndex("dbo.AbstractActivities", new[] { "ActivityTemplateId" });
            DropIndex("dbo.AbstractActivities", new[] { "OwnerId" });
            DropIndex("dbo.AbstractActivities", new[] { "LocationId" });
            DropColumn("dbo.AbpUsers", "ChatRoom_Id");
            DropTable("dbo.AbstractActivityTagAbstractActivities");
            DropTable("dbo.ActivityPlanTagActivityPlans");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Images");
            DropTable("dbo.ChatRooms");
            DropTable("dbo.ChatRoomMessages");
            DropTable("dbo.ActivityParticipants");
            DropTable("dbo.Invitations");
            DropTable("dbo.Locations");
            DropTable("dbo.ActivityPlanTimeSlots");
            DropTable("dbo.Tags",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AbstractActivityTag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ActivityPlanTag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Tag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Ratings");
            DropTable("dbo.Descriptions");
            DropTable("dbo.Replies",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Reply_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ActivityPlans");
            DropTable("dbo.Comments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AbstractActivityComment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ActivityPlanComment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Comment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbstractActivities");
        }
    }
}
