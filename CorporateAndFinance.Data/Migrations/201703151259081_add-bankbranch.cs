namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbankbranch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankBranch",
                c => new
                    {
                        BankBranchID = c.Long(nullable: false, identity: true),
                        BankID = c.Long(nullable: false),
                        BranchName = c.String(),
                        BranchCode = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 50),
                        ContactPerson = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Fax = c.String(maxLength: 50),
                        Address = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BankBranchID)
                .ForeignKey("dbo.Bank", t => t.BankID)
                .Index(t => t.BankID);
            
            AddColumn("dbo.CompanyBank", "BankBranchID", c => c.Long(nullable: false));
            DropColumn("dbo.Bank", "Branch");
            DropColumn("dbo.Bank", "PhoneNumber");
            DropColumn("dbo.Bank", "Email");
            DropColumn("dbo.Bank", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bank", "Address", c => c.String(maxLength: 128));
            AddColumn("dbo.Bank", "Email", c => c.String(maxLength: 50));
            AddColumn("dbo.Bank", "PhoneNumber", c => c.String(maxLength: 50));
            AddColumn("dbo.Bank", "Branch", c => c.String(maxLength: 50));
            DropForeignKey("dbo.BankBranch", "BankID", "dbo.Bank");
            DropIndex("dbo.BankBranch", new[] { "BankID" });
            DropColumn("dbo.CompanyBank", "BankBranchID");
            DropTable("dbo.BankBranch");
        }
    }
}
