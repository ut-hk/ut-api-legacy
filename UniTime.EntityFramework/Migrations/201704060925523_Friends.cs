namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendPairs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LeftId = c.Long(nullable: false),
                        RightId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.LeftId)
                .ForeignKey("dbo.AbpUsers", t => t.RightId)
                .Index(t => t.LeftId)
                .Index(t => t.RightId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendPairs", "RightId", "dbo.AbpUsers");
            DropForeignKey("dbo.FriendPairs", "LeftId", "dbo.AbpUsers");
            DropIndex("dbo.FriendPairs", new[] { "RightId" });
            DropIndex("dbo.FriendPairs", new[] { "LeftId" });
            DropTable("dbo.FriendPairs");
        }
    }
}
