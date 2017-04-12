namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyBankTransactionTransactionStatusField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyBankTransaction", "TransactionStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyBankTransaction", "TransactionStatus");
        }
    }
}
