namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatRoom_Participants : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AbpUsers", "ChatRoom_Id", "dbo.ChatRooms");
            DropIndex("dbo.AbpUsers", new[] { "ChatRoom_Id" });
            CreateTable(
                "dbo.ChatRoomUsers",
                c => new
                    {
                        ChatRoom_Id = c.Guid(nullable: false),
                        User_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChatRoom_Id, t.User_Id })
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoom_Id, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.ChatRoom_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.AbpUsers", "ChatRoom_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbpUsers", "ChatRoom_Id", c => c.Guid());
            DropForeignKey("dbo.ChatRoomUsers", "User_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.ChatRoomUsers", "ChatRoom_Id", "dbo.ChatRooms");
            DropIndex("dbo.ChatRoomUsers", new[] { "User_Id" });
            DropIndex("dbo.ChatRoomUsers", new[] { "ChatRoom_Id" });
            DropTable("dbo.ChatRoomUsers");
            CreateIndex("dbo.AbpUsers", "ChatRoom_Id");
            AddForeignKey("dbo.AbpUsers", "ChatRoom_Id", "dbo.ChatRooms", "Id");
        }
    }
}
