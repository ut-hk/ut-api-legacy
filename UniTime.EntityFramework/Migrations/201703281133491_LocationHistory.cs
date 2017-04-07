namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationHistory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers");
            DropIndex("dbo.Guests", new[] { "OwnerId" });
            CreateTable(
                "dbo.LocationHistories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        GuestId = c.Guid(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .Index(t => t.GuestId);
            
            AlterColumn("dbo.Guests", "OwnerId", c => c.Long(nullable: false));
            CreateIndex("dbo.Guests", "OwnerId");
            AddForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers");
            DropForeignKey("dbo.LocationHistories", "GuestId", "dbo.Guests");
            DropIndex("dbo.LocationHistories", new[] { "GuestId" });
            DropIndex("dbo.Guests", new[] { "OwnerId" });
            AlterColumn("dbo.Guests", "OwnerId", c => c.Long());
            DropTable("dbo.LocationHistories");
            CreateIndex("dbo.Guests", "OwnerId");
            AddForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers", "Id");
        }
    }
}
