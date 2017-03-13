namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityTemplateReferenceTimeSlots : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityTemplateReferenceTimeSlots",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        ActivityTemplate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbstractActivities", t => t.ActivityTemplate_Id)
                .Index(t => t.ActivityTemplate_Id);
            
            DropColumn("dbo.AbstractActivities", "ReferenceStartTime");
            DropColumn("dbo.AbstractActivities", "ReferenceEndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbstractActivities", "ReferenceEndTime", c => c.DateTime());
            AddColumn("dbo.AbstractActivities", "ReferenceStartTime", c => c.DateTime());
            DropForeignKey("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplate_Id", "dbo.AbstractActivities");
            DropIndex("dbo.ActivityTemplateReferenceTimeSlots", new[] { "ActivityTemplate_Id" });
            DropTable("dbo.ActivityTemplateReferenceTimeSlots");
        }
    }
}
