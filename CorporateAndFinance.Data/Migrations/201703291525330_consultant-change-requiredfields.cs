namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class consultantchangerequiredfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Consultant", "LastName", c => c.String(maxLength: 128));
            AlterColumn("dbo.Consultant", "Gender", c => c.String(maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Consultant", "Email", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Consultant", "Email", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Consultant", "Gender", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.Consultant", "LastName", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
