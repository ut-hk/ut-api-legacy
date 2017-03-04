namespace UniTime.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class Location : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.AbstractActivities", "Category_Id", c => c.Long());
            AddColumn("dbo.ActivityPlans", "Category_Id", c => c.Long());
            AddColumn("dbo.Locations", "Coordinate", c => c.Geography());
            AlterColumn("dbo.RouteHistories", "RouteName", c => c.String(nullable: false));
            AlterColumn("dbo.RouteHistories", "Parameters", c => c.String(nullable: false));
            CreateIndex("dbo.AbstractActivities", "Category_Id");
            CreateIndex("dbo.ActivityPlans", "Category_Id");
            AddForeignKey("dbo.ActivityPlans", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.AbstractActivities", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbstractActivities", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ActivityPlans", "Category_Id", "dbo.Categories");
            DropIndex("dbo.ActivityPlans", new[] { "Category_Id" });
            DropIndex("dbo.AbstractActivities", new[] { "Category_Id" });
            AlterColumn("dbo.RouteHistories", "Parameters", c => c.String());
            AlterColumn("dbo.RouteHistories", "RouteName", c => c.String());
            DropColumn("dbo.Locations", "Coordinate");
            DropColumn("dbo.ActivityPlans", "Category_Id");
            DropColumn("dbo.AbstractActivities", "Category_Id");
            DropTable("dbo.Categories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
