namespace UniTime.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Mergerd_Tags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AbstractActivities", "AbstractActivityTag_Id", "dbo.Tags");
            DropForeignKey("dbo.ActivityPlans", "ActivityPlanTag_Id", "dbo.Tags");
            DropForeignKey("dbo.Tags", "ActivityPlan_Id", "dbo.ActivityPlans");
            DropForeignKey("dbo.Tags", "AbstractActivity_Id", "dbo.AbstractActivities");
            DropIndex("dbo.AbstractActivities", new[] { "AbstractActivityTag_Id" });
            DropIndex("dbo.ActivityPlans", new[] { "ActivityPlanTag_Id" });
            DropIndex("dbo.Tags", new[] { "ActivityPlan_Id" });
            DropIndex("dbo.Tags", new[] { "AbstractActivity_Id" });
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
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_AbstractActivityTag_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_ActivityPlanTag_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropColumn("dbo.AbstractActivities", "AbstractActivityTag_Id");
            DropColumn("dbo.ActivityPlans", "ActivityPlanTag_Id");
            DropColumn("dbo.Tags", "Discriminator");
            DropColumn("dbo.Tags", "ActivityPlan_Id");
            DropColumn("dbo.Tags", "AbstractActivity_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "AbstractActivity_Id", c => c.Guid());
            AddColumn("dbo.Tags", "ActivityPlan_Id", c => c.Guid());
            AddColumn("dbo.Tags", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ActivityPlans", "ActivityPlanTag_Id", c => c.Long());
            AddColumn("dbo.AbstractActivities", "AbstractActivityTag_Id", c => c.Long());
            DropForeignKey("dbo.TagActivityPlans", "ActivityPlan_Id", "dbo.ActivityPlans");
            DropForeignKey("dbo.TagActivityPlans", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagAbstractActivities", "AbstractActivity_Id", "dbo.AbstractActivities");
            DropForeignKey("dbo.TagAbstractActivities", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagActivityPlans", new[] { "ActivityPlan_Id" });
            DropIndex("dbo.TagActivityPlans", new[] { "Tag_Id" });
            DropIndex("dbo.TagAbstractActivities", new[] { "AbstractActivity_Id" });
            DropIndex("dbo.TagAbstractActivities", new[] { "Tag_Id" });
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_AbstractActivityTag_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_ActivityPlanTag_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            DropTable("dbo.TagActivityPlans");
            DropTable("dbo.TagAbstractActivities");
            CreateIndex("dbo.Tags", "AbstractActivity_Id");
            CreateIndex("dbo.Tags", "ActivityPlan_Id");
            CreateIndex("dbo.ActivityPlans", "ActivityPlanTag_Id");
            CreateIndex("dbo.AbstractActivities", "AbstractActivityTag_Id");
            AddForeignKey("dbo.Tags", "AbstractActivity_Id", "dbo.AbstractActivities", "Id");
            AddForeignKey("dbo.Tags", "ActivityPlan_Id", "dbo.ActivityPlans", "Id");
            AddForeignKey("dbo.ActivityPlans", "ActivityPlanTag_Id", "dbo.Tags", "Id");
            AddForeignKey("dbo.AbstractActivities", "AbstractActivityTag_Id", "dbo.Tags", "Id");
        }
    }
}
