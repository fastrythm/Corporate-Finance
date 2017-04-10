namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPermissionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPermission",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        PermissionID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPermission", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.UserPermission", new[] { "UserID" });
            DropTable("dbo.UserPermission");
        }
    }
}
