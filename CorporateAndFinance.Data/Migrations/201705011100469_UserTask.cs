namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTaskDetails",
                c => new
                    {
                        UserTaskDetailID = c.Long(nullable: false, identity: true),
                        UserTaskID = c.Long(nullable: false),
                        FromUserID = c.Guid(nullable: false),
                        ToUserID = c.Guid(nullable: false),
                        Remarks = c.String(),
                        Status = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserTaskDetailID)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskID)
                .Index(t => t.UserTaskID);
            
            CreateTable(
                "dbo.UserTasks",
                c => new
                    {
                        UserTaskID = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserTaskID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTaskDetails", "UserTaskID", "dbo.UserTasks");
            DropIndex("dbo.UserTaskDetails", new[] { "UserTaskID" });
            DropTable("dbo.UserTasks");
            DropTable("dbo.UserTaskDetails");
        }
    }
}
