namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTaskTypeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTasks", "Type", c => c.String(maxLength: 25));
            AlterColumn("dbo.UserTaskDetails", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.UserTasks", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserTasks", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserTasks", "Description", c => c.String());
            AlterColumn("dbo.UserTasks", "Title", c => c.String());
            AlterColumn("dbo.UserTaskDetails", "Status", c => c.String());
            DropColumn("dbo.UserTasks", "Type");
        }
    }
}
