namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment_And_Reply_Length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Text", c => c.String(maxLength: 512));
            AlterColumn("dbo.Replies", "Content", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Replies", "Content", c => c.String());
            AlterColumn("dbo.Comments", "Text", c => c.String());
        }
    }
}
