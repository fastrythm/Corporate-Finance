namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companycomplianceamountfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyCompliance", "Amount", c => c.Decimal(precision: 18, scale: 6));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyCompliance", "Amount");
        }
    }
}
