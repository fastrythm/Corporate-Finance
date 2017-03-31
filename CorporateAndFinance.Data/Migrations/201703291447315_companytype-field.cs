namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companytypefield : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Company", "CodeID", "dbo.Code");
            DropIndex("dbo.Company", new[] { "CodeID" });
            AddColumn("dbo.Company", "CompanyType", c => c.String());
            DropColumn("dbo.Company", "CodeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Company", "CodeID", c => c.Long(nullable: false));
            DropColumn("dbo.Company", "CompanyType");
            CreateIndex("dbo.Company", "CodeID");
            AddForeignKey("dbo.Company", "CodeID", "dbo.Code", "CodeID");
        }
    }
}
