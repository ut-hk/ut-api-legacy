namespace UniTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images_to_File_And_Optional_Birthday : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Images", newName: "Files");
            AddColumn("dbo.Files", "OriginalFileName", c => c.String());
            AlterColumn("dbo.UserProfiles", "Birthday", c => c.DateTime());
            DropColumn("dbo.Files", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "FileName", c => c.String());
            AlterColumn("dbo.UserProfiles", "Birthday", c => c.DateTime(nullable: false));
            DropColumn("dbo.Files", "OriginalFileName");
            RenameTable(name: "dbo.Files", newName: "Images");
        }
    }
}
