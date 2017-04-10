namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pettycashChartOfAccountTitleField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PettyCash", "ChartOfAccountTitle", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PettyCash", "ChartOfAccountTitle");
        }
    }
}
