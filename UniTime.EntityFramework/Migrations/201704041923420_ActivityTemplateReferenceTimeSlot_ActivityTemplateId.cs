namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityTemplateReferenceTimeSlot_ActivityTemplateId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplate_Id", "dbo.AbstractActivities");
            DropIndex("dbo.ActivityTemplateReferenceTimeSlots", new[] { "ActivityTemplate_Id" });
            RenameColumn(table: "dbo.ActivityTemplateReferenceTimeSlots", name: "ActivityTemplate_Id", newName: "ActivityTemplateId");
            AlterColumn("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplateId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplateId");
            AddForeignKey("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplateId", "dbo.AbstractActivities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplateId", "dbo.AbstractActivities");
            DropIndex("dbo.ActivityTemplateReferenceTimeSlots", new[] { "ActivityTemplateId" });
            AlterColumn("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplateId", c => c.Guid());
            RenameColumn(table: "dbo.ActivityTemplateReferenceTimeSlots", name: "ActivityTemplateId", newName: "ActivityTemplate_Id");
            CreateIndex("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplate_Id");
            AddForeignKey("dbo.ActivityTemplateReferenceTimeSlots", "ActivityTemplate_Id", "dbo.AbstractActivities", "Id");
        }
    }
}
