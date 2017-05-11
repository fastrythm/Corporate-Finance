namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyBankTransactionCreatedByLastModifiedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyBankTransaction", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.CompanyBankTransaction", "LastModifiedBy", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyBankTransaction", "LastModifiedBy");
            DropColumn("dbo.CompanyBankTransaction", "CreatedBy");
        }
    }
}
