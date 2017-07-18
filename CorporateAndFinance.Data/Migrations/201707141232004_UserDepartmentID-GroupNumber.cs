namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDepartmentIDGroupNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAllocation", "UserDepartmentID", c => c.Long(nullable: false));
            AddColumn("dbo.UserAllocation", "GroupNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAllocation", "GroupNumber");
            DropColumn("dbo.UserAllocation", "UserDepartmentID");
        }
    }
}
