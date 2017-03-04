namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Guest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RouteHistories", "OwnerId", "dbo.AbpUsers");
            DropIndex("dbo.RouteHistories", new[] { "OwnerId" });
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            AddColumn("dbo.RouteHistories", "GuestId", c => c.Guid(nullable: false));
            CreateIndex("dbo.RouteHistories", "GuestId");
            AddForeignKey("dbo.RouteHistories", "GuestId", "dbo.Guests", "Id", cascadeDelete: true);
            DropColumn("dbo.RouteHistories", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RouteHistories", "OwnerId", c => c.Long(nullable: false));
            DropForeignKey("dbo.RouteHistories", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers");
            DropIndex("dbo.RouteHistories", new[] { "GuestId" });
            DropIndex("dbo.Guests", new[] { "OwnerId" });
            DropColumn("dbo.RouteHistories", "GuestId");
            DropTable("dbo.Guests");
            CreateIndex("dbo.RouteHistories", "OwnerId");
            AddForeignKey("dbo.RouteHistories", "OwnerId", "dbo.AbpUsers", "Id", cascadeDelete: true);
        }
    }
}
