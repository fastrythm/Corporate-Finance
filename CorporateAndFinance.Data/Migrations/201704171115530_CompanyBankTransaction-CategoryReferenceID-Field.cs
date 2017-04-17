namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyBankTransactionCategoryReferenceIDField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyBankTransaction", "CategoryReferenceID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyBankTransaction", "CategoryReferenceID");
        }
    }
}
