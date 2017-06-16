namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDepartment_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "DepartmentID", "dbo.Department");
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentID" });
            CreateTable(
                "dbo.UserDepartment",
                c => new
                    {
                        UserDepartmentID = c.Long(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        DepartmentID = c.Long(nullable: false),
                        RoleID = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserDepartmentID)
                .ForeignKey("dbo.Department", t => t.DepartmentID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.DepartmentID);
            
            DropColumn("dbo.AspNetUsers", "DepartmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DepartmentID", c => c.Long());
            DropForeignKey("dbo.UserDepartment", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserDepartment", "DepartmentID", "dbo.Department");
            DropIndex("dbo.UserDepartment", new[] { "DepartmentID" });
            DropIndex("dbo.UserDepartment", new[] { "UserID" });
            DropTable("dbo.UserDepartment");
            CreateIndex("dbo.AspNetUsers", "DepartmentID");
            AddForeignKey("dbo.AspNetUsers", "DepartmentID", "dbo.Department", "DepartmentID");
        }
    }
}
