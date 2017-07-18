namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserExpenseUserBilling : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserExpenses");
            CreateTable(
                "dbo.UserAllocationBilling",
                c => new
                    {
                        UserAllocationBillingID = c.Long(nullable: false, identity: true),
                        UserExpenseID = c.Guid(nullable: false),
                        UserID = c.String(),
                        DepartmentID = c.Long(nullable: false),
                        BillingDate = c.DateTime(nullable: false),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPKR = c.Decimal(nullable: false, precision: 18, scale: 6),
                        AmountUSD = c.Decimal(nullable: false, precision: 18, scale: 6),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserAllocationBillingID)
                .ForeignKey("dbo.Department", t => t.DepartmentID)
                .ForeignKey("dbo.UserExpenses", t => t.UserExpenseID)
                .Index(t => t.UserExpenseID)
                .Index(t => t.DepartmentID);
            
            AddColumn("dbo.UserExpenses", "SerialNumber", c => c.String());
            AlterColumn("dbo.UserExpenses", "UserExpenseID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.UserExpenses", "UserExpenseID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAllocationBilling", "UserExpenseID", "dbo.UserExpenses");
            DropForeignKey("dbo.UserAllocationBilling", "DepartmentID", "dbo.Department");
            DropIndex("dbo.UserAllocationBilling", new[] { "DepartmentID" });
            DropIndex("dbo.UserAllocationBilling", new[] { "UserExpenseID" });
            DropPrimaryKey("dbo.UserExpenses");
            AlterColumn("dbo.UserExpenses", "UserExpenseID", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.UserExpenses", "SerialNumber");
            DropTable("dbo.UserAllocationBilling");
            AddPrimaryKey("dbo.UserExpenses", "UserExpenseID");
        }
    }
}
