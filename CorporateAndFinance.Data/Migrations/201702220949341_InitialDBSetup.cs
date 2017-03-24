namespace CorporateAndFinance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressID = c.Long(nullable: false, identity: true),
                        ReferenceID = c.Guid(nullable: false),
                        CodeID = c.Long(nullable: false),
                        Address = c.String(nullable: false, maxLength: 128),
                        Mobile = c.String(maxLength: 50, unicode: false),
                        Landline = c.String(maxLength: 50, unicode: false),
                        Fax = c.String(maxLength: 50, unicode: false),
                        ZipCode = c.String(maxLength: 20, unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Country = c.String(maxLength: 50),
                        State = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.Bank",
                c => new
                    {
                        BankID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Branch = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Address = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BankID);
            
            CreateTable(
                "dbo.CompanyBank",
                c => new
                    {
                        CompanyBankID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Guid(nullable: false),
                        BankID = c.Long(nullable: false),
                        AccountNumber = c.String(maxLength: 50),
                        AccountType = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyBankID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .ForeignKey("dbo.Bank", t => t.BankID)
                .Index(t => t.CompanyID)
                .Index(t => t.BankID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        CodeID = c.Long(nullable: false),
                        Description = c.String(maxLength: 512),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyID)
                .ForeignKey("dbo.Code", t => t.CodeID)
                .Index(t => t.CodeID);
            
            CreateTable(
                "dbo.Code",
                c => new
                    {
                        CodeID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Type = c.String(nullable: false, maxLength: 128),
                        SortOrder = c.Int(nullable: false),
                        CreatedBy = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CodeID);
            
            CreateTable(
                "dbo.CompanyCompliance",
                c => new
                    {
                        CompanyComplianceID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Guid(nullable: false),
                        LegalAuthorityName = c.String(maxLength: 128),
                        Description = c.String(maxLength: 512),
                        Date = c.DateTime(nullable: false),
                        GeneralRemarks = c.String(maxLength: 512),
                        Status = c.String(nullable: false, maxLength: 50),
                        Remarks1 = c.String(maxLength: 512),
                        Remarks1UserID = c.Guid(),
                        Remarks2 = c.String(maxLength: 512),
                        Remarks2UserID = c.Guid(),
                        FilePath = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CompanyComplianceID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.ConsultantPlacement",
                c => new
                    {
                        ConsultantPlacementID = c.Long(nullable: false, identity: true),
                        VendorConsultantID = c.Long(nullable: false),
                        CodeID = c.Long(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        RateType = c.String(nullable: false, maxLength: 25, unicode: false),
                        ConsultantRate = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CompanyRate = c.Decimal(nullable: false, precision: 18, scale: 6),
                        VendorRate = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConsultantPlacementID)
                .ForeignKey("dbo.VendorConsultant", t => t.VendorConsultantID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.VendorConsultantID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.VendorConsultant",
                c => new
                    {
                        VendorConsultantID = c.Long(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        ConsultantID = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VendorConsultantID)
                .ForeignKey("dbo.Consultant", t => t.ConsultantID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ConsultantID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Consultant",
                c => new
                    {
                        ConsultantID = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 128),
                        Gender = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Mobile = c.String(maxLength: 50, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConsultantID);
            
            CreateTable(
                "dbo.ConsultantDocument",
                c => new
                    {
                        ConsultantDocumentID = c.Long(nullable: false, identity: true),
                        ConsultantID = c.Guid(nullable: false),
                        CodeID = c.Long(nullable: false),
                        DocumentName = c.String(nullable: false, maxLength: 128),
                        DocumentSystemName = c.String(maxLength: 128),
                        SortOrder = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConsultantDocumentID)
                .ForeignKey("dbo.Consultant", t => t.ConsultantID)
                .Index(t => t.ConsultantID);
            
            CreateTable(
                "dbo.UserCardExpense",
                c => new
                    {
                        UserCardExpenseID = c.Long(nullable: false, identity: true),
                        UserCardID = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        RecieptNumber = c.String(maxLength: 50),
                        Description = c.String(maxLength: 128),
                        BillType = c.String(nullable: false, maxLength: 50),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        IsSalesRelated = c.Boolean(nullable: false),
                        TransactionType = c.String(nullable: false, maxLength: 50),
                        Remarks = c.String(maxLength: 512),
                        CompanyReferenceType = c.String(maxLength: 50),
                        CompanyReferenceID = c.Guid(),
                        ExpenseHead = c.String(maxLength: 50),
                        ClientID = c.Guid(),
                        ConsultantID = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserCardExpenseID)
                .ForeignKey("dbo.Consultant", t => t.ConsultantID)
                .ForeignKey("dbo.UserCard", t => t.UserCardID)
                .ForeignKey("dbo.Company", t => t.CompanyReferenceID)
                .Index(t => t.UserCardID)
                .Index(t => t.CompanyReferenceID)
                .Index(t => t.ConsultantID);
            
            CreateTable(
                "dbo.UserCard",
                c => new
                    {
                        UserCardID = c.Long(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        CardType = c.String(nullable: false, maxLength: 50),
                        CardNumber = c.String(nullable: false, maxLength: 50),
                        CardMemberTitle = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserCardID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(maxLength: 128),
                        EmployeeNumber = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.UserCompany",
                c => new
                    {
                        UserCompanyID = c.Long(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserCompanyID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.CompanyID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CompanyBankPosition",
                c => new
                    {
                        CompanyBankPositionID = c.Long(nullable: false, identity: true),
                        CompanyBankID = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                    })
                .PrimaryKey(t => t.CompanyBankPositionID)
                .ForeignKey("dbo.CompanyBank", t => t.CompanyBankID)
                .Index(t => t.CompanyBankID);
            
            CreateTable(
                "dbo.CompanyBankTransaction",
                c => new
                    {
                        CompanyBankTransactionID = c.Long(nullable: false, identity: true),
                        CompanyBankID = c.Long(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionType = c.String(nullable: false, maxLength: 50),
                        PaymentType = c.String(nullable: false, maxLength: 50),
                        ReceiptNumber = c.String(maxLength: 50),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Description = c.String(maxLength: 128),
                        CategoryType = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyBankTransactionID)
                .ForeignKey("dbo.CompanyBank", t => t.CompanyBankID)
                .Index(t => t.CompanyBankID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Gadgets",
                c => new
                    {
                        GadgetID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GadgetID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.PettyCash",
                c => new
                    {
                        PettyCashID = c.Long(nullable: false, identity: true),
                        Description = c.String(maxLength: 128),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionType = c.String(nullable: false, maxLength: 50, unicode: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        IsDeleted = c.Boolean(nullable: false),
                        UserID = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PettyCashID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.Gadgets", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CompanyBank", "BankID", "dbo.Bank");
            DropForeignKey("dbo.CompanyBankTransaction", "CompanyBankID", "dbo.CompanyBank");
            DropForeignKey("dbo.CompanyBankPosition", "CompanyBankID", "dbo.CompanyBank");
            DropForeignKey("dbo.UserCompany", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.UserCardExpense", "CompanyReferenceID", "dbo.Company");
            DropForeignKey("dbo.ConsultantPlacement", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.VendorConsultant", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCompany", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCard", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConsultantPlacement", "VendorConsultantID", "dbo.VendorConsultant");
            DropForeignKey("dbo.VendorConsultant", "ConsultantID", "dbo.Consultant");
            DropForeignKey("dbo.UserCardExpense", "UserCardID", "dbo.UserCard");
            DropForeignKey("dbo.UserCardExpense", "ConsultantID", "dbo.Consultant");
            DropForeignKey("dbo.ConsultantDocument", "ConsultantID", "dbo.Consultant");
            DropForeignKey("dbo.CompanyCompliance", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.CompanyBank", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Company", "CodeID", "dbo.Code");
            DropIndex("dbo.Gadgets", new[] { "CategoryID" });
            DropIndex("dbo.CompanyBankTransaction", new[] { "CompanyBankID" });
            DropIndex("dbo.CompanyBankPosition", new[] { "CompanyBankID" });
            DropIndex("dbo.UserCompany", new[] { "User_Id" });
            DropIndex("dbo.UserCompany", new[] { "CompanyID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserCard", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserCardExpense", new[] { "ConsultantID" });
            DropIndex("dbo.UserCardExpense", new[] { "CompanyReferenceID" });
            DropIndex("dbo.UserCardExpense", new[] { "UserCardID" });
            DropIndex("dbo.ConsultantDocument", new[] { "ConsultantID" });
            DropIndex("dbo.VendorConsultant", new[] { "User_Id" });
            DropIndex("dbo.VendorConsultant", new[] { "ConsultantID" });
            DropIndex("dbo.ConsultantPlacement", new[] { "CompanyID" });
            DropIndex("dbo.ConsultantPlacement", new[] { "VendorConsultantID" });
            DropIndex("dbo.CompanyCompliance", new[] { "CompanyID" });
            DropIndex("dbo.Company", new[] { "CodeID" });
            DropIndex("dbo.CompanyBank", new[] { "BankID" });
            DropIndex("dbo.CompanyBank", new[] { "CompanyID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PettyCash");
            DropTable("dbo.Gadgets");
            DropTable("dbo.Categories");
            DropTable("dbo.CompanyBankTransaction");
            DropTable("dbo.CompanyBankPosition");
            DropTable("dbo.UserCompany");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserCard");
            DropTable("dbo.UserCardExpense");
            DropTable("dbo.ConsultantDocument");
            DropTable("dbo.Consultant");
            DropTable("dbo.VendorConsultant");
            DropTable("dbo.ConsultantPlacement");
            DropTable("dbo.CompanyCompliance");
            DropTable("dbo.Code");
            DropTable("dbo.Company");
            DropTable("dbo.CompanyBank");
            DropTable("dbo.Bank");
            DropTable("dbo.Address");
        }
    }
}
