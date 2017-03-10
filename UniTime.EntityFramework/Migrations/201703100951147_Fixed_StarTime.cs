namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed_StarTime : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.AbstractActivities", "ReferenceStarTime", "ReferenceStartTime");
            RenameColumn("dbo.AbstractActivities", "StarTime", "StartTime");
        }

        public override void Down()
        {
            RenameColumn("dbo.AbstractActivities", "ReferenceStartTime", "ReferenceStarTime");
            RenameColumn("dbo.AbstractActivities", "StartTime", "StarTime");
        }
    }
}
