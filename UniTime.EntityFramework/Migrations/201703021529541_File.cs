namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class File : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "FileName", c => c.String());
            AddColumn("dbo.Images", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.Images", "LastModifierUserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "LastModifierUserId");
            DropColumn("dbo.Images", "LastModificationTime");
            DropColumn("dbo.Images", "FileName");
        }
    }
}
