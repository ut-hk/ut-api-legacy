namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestOwner : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers");
            DropIndex("dbo.Guests", new[] { "OwnerId" });
            AlterColumn("dbo.Guests", "OwnerId", c => c.Long());
            CreateIndex("dbo.Guests", "OwnerId");
            AddForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers");
            DropIndex("dbo.Guests", new[] { "OwnerId" });
            AlterColumn("dbo.Guests", "OwnerId", c => c.Long(nullable: false));
            CreateIndex("dbo.Guests", "OwnerId");
            AddForeignKey("dbo.Guests", "OwnerId", "dbo.AbpUsers", "Id", cascadeDelete: true);
        }
    }
}
