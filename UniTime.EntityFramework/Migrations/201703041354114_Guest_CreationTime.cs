namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Guest_CreationTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "CreationTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "CreationTime");
        }
    }
}
