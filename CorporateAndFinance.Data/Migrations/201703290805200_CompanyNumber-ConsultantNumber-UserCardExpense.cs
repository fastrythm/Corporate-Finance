namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyNumberConsultantNumberUserCardExpense : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserCardExpense", name: "CompanyReferenceID", newName: "Company_CompanyID");
            RenameIndex(table: "dbo.UserCardExpense", name: "IX_CompanyReferenceID", newName: "IX_Company_CompanyID");
            AddColumn("dbo.Company", "ShortName", c => c.String(maxLength: 50));
            AddColumn("dbo.Company", "CompanyNumber", c => c.Long(nullable: false));
            AddColumn("dbo.Consultant", "ConsultantNumber", c => c.Int(nullable: false));
            AddColumn("dbo.UserCardExpense", "PaymentDetail", c => c.String(maxLength: 512));
            DropColumn("dbo.UserCardExpense", "CompanyReferenceType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserCardExpense", "CompanyReferenceType", c => c.String(maxLength: 50));
            DropColumn("dbo.UserCardExpense", "PaymentDetail");
            DropColumn("dbo.Consultant", "ConsultantNumber");
            DropColumn("dbo.Company", "CompanyNumber");
            DropColumn("dbo.Company", "ShortName");
            RenameIndex(table: "dbo.UserCardExpense", name: "IX_Company_CompanyID", newName: "IX_CompanyReferenceID");
            RenameColumn(table: "dbo.UserCardExpense", name: "Company_CompanyID", newName: "CompanyReferenceID");
        }
    }
}
