namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SLAApprovalnewtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SLAApproval",
                c => new
                    {
                        SLAApprovalID = c.Long(nullable: false, identity: true),
                        DepartmentID = c.Long(nullable: false),
                        UserID = c.String(),
                        SLAType = c.String(),
                        Description = c.String(),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SLAApprovalID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SLAApproval");
        }
    }
}
