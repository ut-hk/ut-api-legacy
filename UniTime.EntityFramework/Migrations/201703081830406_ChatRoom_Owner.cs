namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatRoom_Owner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatRooms", "OwnerId", c => c.Long(nullable: false));
            CreateIndex("dbo.Tags", "Text");
            CreateIndex("dbo.ChatRooms", "OwnerId");
            AddForeignKey("dbo.ChatRooms", "OwnerId", "dbo.AbpUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatRooms", "OwnerId", "dbo.AbpUsers");
            DropIndex("dbo.ChatRooms", new[] { "OwnerId" });
            DropIndex("dbo.Tags", new[] { "Text" });
            DropColumn("dbo.ChatRooms", "OwnerId");
        }
    }
}
