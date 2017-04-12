namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComapnyBankTransactionToCompanyBankIDField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyBankTransaction", "ToCompanyBankID", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyBankTransaction", "ToCompanyBankID");
        }
    }
}
