namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationId_in_AbstractActivity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AbstractActivities", "LocationId", "dbo.Locations");
            DropIndex("dbo.AbstractActivities", new[] { "LocationId" });
            AlterColumn("dbo.AbstractActivities", "LocationId", c => c.Guid());
            CreateIndex("dbo.AbstractActivities", "LocationId");
            AddForeignKey("dbo.AbstractActivities", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbstractActivities", "LocationId", "dbo.Locations");
            DropIndex("dbo.AbstractActivities", new[] { "LocationId" });
            AlterColumn("dbo.AbstractActivities", "LocationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.AbstractActivities", "LocationId");
            AddForeignKey("dbo.AbstractActivities", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
    }
}
