namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Descriptions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AbstractActivityImages", "AbstractActivity_Id", "dbo.AbstractActivities");
            DropForeignKey("dbo.AbstractActivityImages", "Image_Id", "dbo.Files");
            DropIndex("dbo.AbstractActivityImages", new[] { "AbstractActivity_Id" });
            DropIndex("dbo.AbstractActivityImages", new[] { "Image_Id" });
            AddColumn("dbo.Descriptions", "AbstractActivityId", c => c.Guid());
            CreateIndex("dbo.Descriptions", "AbstractActivityId");
            AddForeignKey("dbo.Descriptions", "AbstractActivityId", "dbo.AbstractActivities", "Id");
            DropColumn("dbo.AbstractActivities", "Description");
            DropTable("dbo.AbstractActivityImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AbstractActivityImages",
                c => new
                    {
                        AbstractActivity_Id = c.Guid(nullable: false),
                        Image_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AbstractActivity_Id, t.Image_Id });
            
            AddColumn("dbo.AbstractActivities", "Description", c => c.String());
            DropForeignKey("dbo.Descriptions", "AbstractActivityId", "dbo.AbstractActivities");
            DropIndex("dbo.Descriptions", new[] { "AbstractActivityId" });
            DropColumn("dbo.Descriptions", "AbstractActivityId");
            CreateIndex("dbo.AbstractActivityImages", "Image_Id");
            CreateIndex("dbo.AbstractActivityImages", "AbstractActivity_Id");
            AddForeignKey("dbo.AbstractActivityImages", "Image_Id", "dbo.Files", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AbstractActivityImages", "AbstractActivity_Id", "dbo.AbstractActivities", "Id", cascadeDelete: true);
        }
    }
}
