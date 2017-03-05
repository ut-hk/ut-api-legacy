namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tag_Length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Text", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Text", c => c.String());
        }
    }
}
