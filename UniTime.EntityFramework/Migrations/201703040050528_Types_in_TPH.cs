namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Types_in_TPH : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Descriptions", "Type");
            DropColumn("dbo.ChatRoomMessages", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatRoomMessages", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Descriptions", "Type", c => c.Int(nullable: false));
        }
    }
}
