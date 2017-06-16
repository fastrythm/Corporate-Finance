namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionMandatoryFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requisition", "JobTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Requisition", "GradeLevel", c => c.String(nullable: false));
            AlterColumn("dbo.Requisition", "ReportedToJobTitle", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requisition", "ReportedToJobTitle", c => c.String());
            AlterColumn("dbo.Requisition", "GradeLevel", c => c.String());
            AlterColumn("dbo.Requisition", "JobTitle", c => c.String());
        }
    }
}
