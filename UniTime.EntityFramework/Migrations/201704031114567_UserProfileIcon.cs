namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfileIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "IconId", c => c.Guid());
            CreateIndex("dbo.UserProfiles", "IconId");
            AddForeignKey("dbo.UserProfiles", "IconId", "dbo.Files", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "IconId", "dbo.Files");
            DropIndex("dbo.UserProfiles", new[] { "IconId" });
            DropColumn("dbo.UserProfiles", "IconId");
        }
    }
}
