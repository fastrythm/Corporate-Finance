namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequistionPersonReplacementField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisition", "RevisedOrganogram", c => c.String());
            AddColumn("dbo.Requisition", "ReplacePersonName", c => c.String());
            AddColumn("dbo.Requisition", "ReplacePersonJobTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requisition", "ReplacePersonJobTitle");
            DropColumn("dbo.Requisition", "ReplacePersonName");
            DropColumn("dbo.Requisition", "RevisedOrganogram");
        }
    }
}
