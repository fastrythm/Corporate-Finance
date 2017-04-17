namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyBankTransactionCategoryTypeMandatory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CompanyBankTransaction", "CategoryType", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CompanyBankTransaction", "CategoryType", c => c.String(maxLength: 128));
        }
    }
}
