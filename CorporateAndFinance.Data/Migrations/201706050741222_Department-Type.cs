namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "DepartmentType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Department", "DepartmentType");
        }
    }
}
