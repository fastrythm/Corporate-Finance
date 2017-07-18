namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAllocationRequestedDepartmentIDField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAllocation", "RequestedDepartmentID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAllocation", "RequestedDepartmentID");
        }
    }
}
