namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Description : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Descriptions", "ActivityPlanId", "dbo.ActivityPlans");
            AddForeignKey("dbo.Descriptions", "ActivityPlanId", "dbo.ActivityPlans", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Descriptions", "ActivityPlanId", "dbo.ActivityPlans");
            AddForeignKey("dbo.Descriptions", "ActivityPlanId", "dbo.ActivityPlans", "Id", cascadeDelete: true);
        }
    }
}
