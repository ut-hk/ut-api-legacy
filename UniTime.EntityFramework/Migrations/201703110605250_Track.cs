namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Track : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FromId = c.Long(nullable: false),
                        ToId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.FromId)
                .ForeignKey("dbo.AbpUsers", t => t.ToId)
                .Index(t => t.FromId)
                .Index(t => t.ToId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "ToId", "dbo.AbpUsers");
            DropForeignKey("dbo.Tracks", "FromId", "dbo.AbpUsers");
            DropIndex("dbo.Tracks", new[] { "ToId" });
            DropIndex("dbo.Tracks", new[] { "FromId" });
            DropTable("dbo.Tracks");
        }
    }
}
