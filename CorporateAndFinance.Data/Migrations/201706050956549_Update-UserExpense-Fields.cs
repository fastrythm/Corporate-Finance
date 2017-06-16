namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserExpenseFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserExpenses", "ExpenseID", "dbo.Expenses");
            DropIndex("dbo.UserExpenses", new[] { "ExpenseID" });
            AddColumn("dbo.UserExpenses", "Monthly_Salary", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Monthly_Salary2", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "EOBI_Employer", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "PF_Employer", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Mobile_Allowance", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Bonus", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Meal_Reimbursement", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Transportation", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Leave_Encashment", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Incentive_PSM", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Health_Insurance", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Medical_OPD", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Billable_Salary_PKR", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "Billable_Salary_USD", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            DropColumn("dbo.UserExpenses", "ExpenseID");
            DropColumn("dbo.UserExpenses", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserExpenses", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.UserExpenses", "ExpenseID", c => c.Long(nullable: false));
            DropColumn("dbo.UserExpenses", "Billable_Salary_USD");
            DropColumn("dbo.UserExpenses", "Billable_Salary_PKR");
            DropColumn("dbo.UserExpenses", "Medical_OPD");
            DropColumn("dbo.UserExpenses", "Health_Insurance");
            DropColumn("dbo.UserExpenses", "Incentive_PSM");
            DropColumn("dbo.UserExpenses", "Leave_Encashment");
            DropColumn("dbo.UserExpenses", "Transportation");
            DropColumn("dbo.UserExpenses", "Meal_Reimbursement");
            DropColumn("dbo.UserExpenses", "Bonus");
            DropColumn("dbo.UserExpenses", "Mobile_Allowance");
            DropColumn("dbo.UserExpenses", "PF_Employer");
            DropColumn("dbo.UserExpenses", "EOBI_Employer");
            DropColumn("dbo.UserExpenses", "Monthly_Salary2");
            DropColumn("dbo.UserExpenses", "Monthly_Salary");
            CreateIndex("dbo.UserExpenses", "ExpenseID");
            AddForeignKey("dbo.UserExpenses", "ExpenseID", "dbo.Expenses", "ExpenseID");
        }
    }
}
