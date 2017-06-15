namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Requisition_UserAllocation_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequisitionApproval",
                c => new
                    {
                        RequisitionApprovalID = c.Long(nullable: false, identity: true),
                        RequisitionID = c.Long(nullable: false),
                        DepartmentID = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Status = c.String(),
                        Comments = c.String(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RequisitionApprovalID)
                .ForeignKey("dbo.Department", t => t.DepartmentID)
                .ForeignKey("dbo.Requisition", t => t.RequisitionID)
                .Index(t => t.RequisitionID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Requisition",
                c => new
                    {
                        RequisitionID = c.Long(nullable: false, identity: true),
                        IsBudgeted = c.Boolean(nullable: false),
                        RequisitionDate = c.DateTime(nullable: false),
                        RequestNumber = c.String(),
                        DepartmentID = c.Long(nullable: false),
                        ClientServices = c.String(),
                        Division = c.String(),
                        NewProject = c.String(),
                        Tenure = c.String(),
                        StartDate = c.DateTime(),
                        JobTitle = c.String(),
                        GradeLevel = c.String(),
                        ReportedToJobTitle = c.String(),
                        IsTravelling = c.Boolean(nullable: false),
                        NoOfPosition = c.Int(nullable: false),
                        AdditionalBenefits = c.String(),
                        TypeOfEmployement = c.String(),
                        Duration = c.String(),
                        IsNewPosition = c.Boolean(nullable: false),
                        Reason = c.String(),
                        JobProfileFilePath = c.String(),
                        OtherDetail = c.String(),
                        IsBillable = c.Boolean(nullable: false),
                        ProjectedRevenue = c.Decimal(nullable: false, precision: 18, scale: 6),
                        AllocatedBudget = c.Decimal(nullable: false, precision: 18, scale: 6),
                        HasLaptopPC = c.Boolean(nullable: false),
                        LaptopDetails = c.String(),
                        HasHeadset = c.Boolean(nullable: false),
                        HasMouse = c.Boolean(nullable: false),
                        HasEmail = c.Boolean(nullable: false),
                        EmailDetails = c.String(),
                        HasUSDID = c.Boolean(nullable: false),
                        HasInternetAccess = c.Boolean(nullable: false),
                        InternetAccessDetails = c.String(),
                        ApplicationSoftwareSecurityDetails = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Status = c.String(),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RequisitionID);
            
            CreateTable(
                "dbo.UserAllocation",
                c => new
                    {
                        UserAllocationID = c.Long(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        DepartmentID = c.Long(nullable: false),
                        RequisitionID = c.Long(),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 6),
                        IsActive = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Status = c.String(),
                        Comments = c.String(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserAllocationID)
                .ForeignKey("dbo.Department", t => t.DepartmentID)
                .ForeignKey("dbo.Requisition", t => t.RequisitionID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.DepartmentID)
                .Index(t => t.RequisitionID);
            
            AddColumn("dbo.AspNetUsers", "RequisitionID", c => c.Long());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAllocation", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAllocation", "RequisitionID", "dbo.Requisition");
            DropForeignKey("dbo.UserAllocation", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.RequisitionApproval", "RequisitionID", "dbo.Requisition");
            DropForeignKey("dbo.RequisitionApproval", "DepartmentID", "dbo.Department");
            DropIndex("dbo.UserAllocation", new[] { "RequisitionID" });
            DropIndex("dbo.UserAllocation", new[] { "DepartmentID" });
            DropIndex("dbo.UserAllocation", new[] { "UserID" });
            DropIndex("dbo.RequisitionApproval", new[] { "DepartmentID" });
            DropIndex("dbo.RequisitionApproval", new[] { "RequisitionID" });
            DropColumn("dbo.AspNetUsers", "RequisitionID");
            DropTable("dbo.UserAllocation");
            DropTable("dbo.Requisition");
            DropTable("dbo.RequisitionApproval");
        }
    }
}
