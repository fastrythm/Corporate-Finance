namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTaskDetailIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTaskDetails", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTaskDetails", "IsActive");
        }
    }
}
