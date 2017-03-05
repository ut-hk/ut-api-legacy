using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;

namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityPlanTagActivityPlans", "ActivityPlanTag_Id", "dbo.Tags");
            DropForeignKey("dbo.ActivityPlanTagActivityPlans", "ActivityPlan_Id", "dbo.ActivityPlans");
            DropForeignKey("dbo.AbstractActivityTagAbstractActivities", "AbstractActivityTag_Id", "dbo.Tags");
            DropForeignKey("dbo.AbstractActivityTagAbstractActivities", "AbstractActivity_Id", "dbo.AbstractActivities");
            DropIndex("dbo.ActivityPlanTagActivityPlans", new[] { "ActivityPlanTag_Id" });
            DropIndex("dbo.ActivityPlanTagActivityPlans", new[] { "ActivityPlan_Id" });
            DropIndex("dbo.AbstractActivityTagAbstractActivities", new[] { "AbstractActivityTag_Id" });
            DropIndex("dbo.AbstractActivityTagAbstractActivities", new[] { "AbstractActivity_Id" });
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ActivityPlan_Id = c.Guid(),
                        AbstractActivity_Id = c.Guid(),
                    }, new Dictionary<string, AnnotationValues>());
            
            AddColumn("dbo.AbstractActivities", "AbstractActivityTag_Id", c => c.Long());
            AddColumn("dbo.ActivityPlans", "ActivityPlanTag_Id", c => c.Long());
            AddColumn("dbo.Tags", "ActivityPlan_Id", c => c.Guid());
            AddColumn("dbo.Tags", "AbstractActivity_Id", c => c.Guid());
            CreateIndex("dbo.AbstractActivities", "AbstractActivityTag_Id");
            CreateIndex("dbo.ActivityPlans", "ActivityPlanTag_Id");
            CreateIndex("dbo.Tags", "ActivityPlan_Id");
            CreateIndex("dbo.Tags", "AbstractActivity_Id");
            AddForeignKey("dbo.AbstractActivities", "AbstractActivityTag_Id", "dbo.Tags", "Id");
            AddForeignKey("dbo.ActivityPlans", "ActivityPlanTag_Id", "dbo.Tags", "Id");
            AddForeignKey("dbo.Tags", "ActivityPlan_Id", "dbo.ActivityPlans", "Id");
            AddForeignKey("dbo.Tags", "AbstractActivity_Id", "dbo.AbstractActivities", "Id");
            DropTable("dbo.ActivityPlanTagActivityPlans");
            DropTable("dbo.AbstractActivityTagAbstractActivities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AbstractActivityTagAbstractActivities",
                c => new
                    {
                        AbstractActivityTag_Id = c.Long(nullable: false),
                        AbstractActivity_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AbstractActivityTag_Id, t.AbstractActivity_Id });
            
            CreateTable(
                "dbo.ActivityPlanTagActivityPlans",
                c => new
                    {
                        ActivityPlanTag_Id = c.Long(nullable: false),
                        ActivityPlan_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActivityPlanTag_Id, t.ActivityPlan_Id });
            
            DropForeignKey("dbo.Tags", "AbstractActivity_Id", "dbo.AbstractActivities");
            DropForeignKey("dbo.Tags", "ActivityPlan_Id", "dbo.ActivityPlans");
            DropForeignKey("dbo.ActivityPlans", "ActivityPlanTag_Id", "dbo.Tags");
            DropForeignKey("dbo.AbstractActivities", "AbstractActivityTag_Id", "dbo.Tags");
            DropIndex("dbo.Tags", new[] { "AbstractActivity_Id" });
            DropIndex("dbo.Tags", new[] { "ActivityPlan_Id" });
            DropIndex("dbo.ActivityPlans", new[] { "ActivityPlanTag_Id" });
            DropIndex("dbo.AbstractActivities", new[] { "AbstractActivityTag_Id" });
            DropColumn("dbo.Tags", "AbstractActivity_Id");
            DropColumn("dbo.Tags", "ActivityPlan_Id");
            DropColumn("dbo.ActivityPlans", "ActivityPlanTag_Id");
            DropColumn("dbo.AbstractActivities", "AbstractActivityTag_Id");
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ActivityPlan_Id = c.Guid(),
                        AbstractActivity_Id = c.Guid(),
                    },new Dictionary<string, AnnotationValues>());
            
            CreateIndex("dbo.AbstractActivityTagAbstractActivities", "AbstractActivity_Id");
            CreateIndex("dbo.AbstractActivityTagAbstractActivities", "AbstractActivityTag_Id");
            CreateIndex("dbo.ActivityPlanTagActivityPlans", "ActivityPlan_Id");
            CreateIndex("dbo.ActivityPlanTagActivityPlans", "ActivityPlanTag_Id");
            AddForeignKey("dbo.AbstractActivityTagAbstractActivities", "AbstractActivity_Id", "dbo.AbstractActivities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AbstractActivityTagAbstractActivities", "AbstractActivityTag_Id", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ActivityPlanTagActivityPlans", "ActivityPlan_Id", "dbo.ActivityPlans", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ActivityPlanTagActivityPlans", "ActivityPlanTag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
