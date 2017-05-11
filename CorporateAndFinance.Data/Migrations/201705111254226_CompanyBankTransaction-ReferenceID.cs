namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyBankTransactionReferenceID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyBankTransaction", "ReferenceID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyBankTransaction", "ReferenceID");
        }
    }
}
