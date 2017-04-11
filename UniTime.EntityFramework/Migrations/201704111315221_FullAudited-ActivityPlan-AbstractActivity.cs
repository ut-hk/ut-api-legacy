namespace UniTime.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class FullAuditedActivityPlanAbstractActivity : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.AbstractActivities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        LocationId = c.Guid(),
                        OwnerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        ActivityTemplateId = c.Guid(),
                        ReferenceId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Category_Id = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_AbstractActivity_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_Activity_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_ActivityTemplate_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.ActivityPlans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        OwnerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Category_Id = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ActivityPlan_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.AbstractActivities", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbstractActivities", "DeleterUserId", c => c.Long());
            AddColumn("dbo.AbstractActivities", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.ActivityPlans", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ActivityPlans", "DeleterUserId", c => c.Long());
            AddColumn("dbo.ActivityPlans", "DeletionTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityPlans", "DeletionTime");
            DropColumn("dbo.ActivityPlans", "DeleterUserId");
            DropColumn("dbo.ActivityPlans", "IsDeleted");
            DropColumn("dbo.AbstractActivities", "DeletionTime");
            DropColumn("dbo.AbstractActivities", "DeleterUserId");
            DropColumn("dbo.AbstractActivities", "IsDeleted");
            AlterTableAnnotations(
                "dbo.ActivityPlans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        OwnerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Category_Id = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ActivityPlan_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.AbstractActivities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        LocationId = c.Guid(),
                        OwnerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        ActivityTemplateId = c.Guid(),
                        ReferenceId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Category_Id = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_AbstractActivity_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_Activity_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_ActivityTemplate_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
