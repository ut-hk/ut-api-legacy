namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Description_HTMLClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Descriptions", "HTMLClasses", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Descriptions", "HTMLClasses");
        }
    }
}
