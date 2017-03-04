namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RouteHistories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RouteName = c.String(),
                        Parameters = c.String(),
                        Referer = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        OwnerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteHistories", "OwnerId", "dbo.AbpUsers");
            DropIndex("dbo.RouteHistories", new[] { "OwnerId" });
            DropTable("dbo.RouteHistories");
        }
    }
}
