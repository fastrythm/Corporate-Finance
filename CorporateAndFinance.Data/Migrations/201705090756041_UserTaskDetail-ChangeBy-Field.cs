namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTaskDetailChangeByField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTaskDetails", "ChangeBy", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTaskDetails", "ChangeBy");
        }
    }
}
