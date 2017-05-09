namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankTransactionMandatorFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CompanyBankTransaction", "ReceiptNumber", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CompanyBankTransaction", "ReceiptNumber", c => c.String(maxLength: 50));
        }
    }
}
