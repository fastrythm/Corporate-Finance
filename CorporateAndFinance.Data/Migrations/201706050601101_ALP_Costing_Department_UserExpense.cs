namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALP_Costing_Department_UserExpense : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gadgets", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Gadgets", new[] { "CategoryID" });
            //CreateTable(
            //    "dbo.UserExpenses",
            //    c => new
            //        {
            //            UserExpenseID = c.Long(nullable: false, identity: true),
            //            UserID = c.String(maxLength: 128),
            //            DepartmentID = c.Long(nullable: false),
            //            ExpenseID = c.Long(nullable: false),
            //            ExpenseDate = c.DateTime(nullable: false),
            //            Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
            //            IsActive = c.Boolean(nullable: false),
            //            CreatedBy = c.Guid(nullable: false),
            //            CreatedOn = c.DateTime(nullable: false),
            //            LastModified = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.UserExpenseID)
            //    .ForeignKey("dbo.Department", t => t.DepartmentID)
            //    .ForeignKey("dbo.Expenses", t => t.ExpenseID)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserID)
            //    .Index(t => t.UserID)
            //    .Index(t => t.DepartmentID)
            //    .Index(t => t.ExpenseID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        ShortName = c.String(maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ExpenseID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Type = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ExpenseID);
            
            AddColumn("dbo.AspNetUsers", "DepartmentID", c => c.Long());
            CreateIndex("dbo.AspNetUsers", "DepartmentID");
            AddForeignKey("dbo.AspNetUsers", "DepartmentID", "dbo.Department", "DepartmentID");
            DropColumn("dbo.AspNetUsers", "Department");
            DropTable("dbo.Categories");
            DropTable("dbo.Gadgets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Gadgets",
                c => new
                    {
                        GadgetID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GadgetID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            AddColumn("dbo.AspNetUsers", "Department", c => c.String());
            DropForeignKey("dbo.UserExpenses", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserExpenses", "ExpenseID", "dbo.Expenses");
            DropForeignKey("dbo.AspNetUsers", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.UserExpenses", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "CompanyID", "dbo.Company");
            DropIndex("dbo.Department", new[] { "CompanyID" });
            DropIndex("dbo.UserExpenses", new[] { "ExpenseID" });
            DropIndex("dbo.UserExpenses", new[] { "DepartmentID" });
            DropIndex("dbo.UserExpenses", new[] { "UserID" });
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentID" });
            DropColumn("dbo.AspNetUsers", "DepartmentID");
            DropTable("dbo.Expenses");
            DropTable("dbo.Department");
            DropTable("dbo.UserExpenses");
            CreateIndex("dbo.Gadgets", "CategoryID");
            AddForeignKey("dbo.Gadgets", "CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
