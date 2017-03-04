namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name_in_ChatRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatRooms", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatRooms", "Name");
        }
    }
}
