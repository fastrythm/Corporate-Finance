namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseSortOrderField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "SortOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenses", "SortOrder");
        }
    }
}
