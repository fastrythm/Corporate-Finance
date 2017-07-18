using CorporateAndFinance.Core.Model;

using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Data
{
    public class CorporateFinanceEntities : DbContext
    {
        public CorporateFinanceEntities() : base("CorporateFinanceConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }


        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankBranch> BankBranches { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyBank> CompanyBanks { get; set; }
        public virtual DbSet<CompanyBankPosition> CompanyBankPositions { get; set; }
        public virtual DbSet<CompanyBankTransaction> CompanyBankTransactions { get; set; }
        public virtual DbSet<CompanyCompliance> CompanyCompliances { get; set; }
        public virtual DbSet<Consultant> Consultants { get; set; }
        public virtual DbSet<ConsultantDocument> ConsultantDocuments { get; set; }
        public virtual DbSet<ConsultantPlacement> ConsultantPlacements { get; set; }
        public virtual DbSet<PettyCash> PettyCashes { get; set; }

        public virtual DbSet<UserCard> UserCards { get; set; }
        public virtual DbSet<UserCardExpense> UserCardExpenses { get; set; }
        public virtual DbSet<UserCompany> UserCompanies { get; set; }
        public virtual DbSet<VendorConsultant> VendorConsultants { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<UserTaskDetail> UserTaskDetails { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<UserExpense> UserExpenses { get; set; }
        [NotMapped]
        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<UserDepartment> UserDepartments { get; set; }

        public virtual DbSet<Requisition> Requisitions { get; set; }
        public virtual DbSet<UserAllocation> UserAllocations { get; set; }
        public virtual DbSet<RequisitionApproval> RequisitionApprovals { get; set; }
        public virtual DbSet<SLAApproval> SLAApprovals { get; set; }

        public virtual DbSet<UserAllocationBilling> UserAllocationBillings { get; set; }
        
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //   modelBuilder.Configurations.Add(new GadgetConfiguration());
            //   modelBuilder.Configurations.Add(new CategoryConfiguration());


            modelBuilder.Entity<Address>()
              .Property(e => e.Mobile)
              .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Landline)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.CompanyBanks)
                .WithRequired(e => e.Bank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyBanks)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyCompliances)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTask>()
              .HasMany(e => e.UserTaskDetails)
              .WithRequired(e => e.UserTask)
              .WillCascadeOnDelete(false);



            //modelBuilder.Entity<Company>()
            //    .HasMany(e => e.ConsultantPlacements)
            //    .WithRequired(e => e.Company)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.UserCompanies)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyBank>()
                .HasMany(e => e.CompanyBankPositions)
                .WithRequired(e => e.CompanyBank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyBank>()
                .HasMany(e => e.CompanyBankTransactions)
                .WithRequired(e => e.CompanyBank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyBankPosition>()
                .Property(e => e.Amount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CompanyBankTransaction>()
                .Property(e => e.Amount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Consultant>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<Consultant>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Consultant>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Consultant>()
                .HasMany(e => e.ConsultantDocuments)
                .WithRequired(e => e.Consultant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Consultant>()
                .HasMany(e => e.VendorConsultants)
                .WithRequired(e => e.Consultant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConsultantPlacement>()
                .Property(e => e.RateType)
                .IsUnicode(false);

            modelBuilder.Entity<ConsultantPlacement>()
                .Property(e => e.ConsultantRate)
                .HasPrecision(18, 6);

            modelBuilder.Entity<ConsultantPlacement>()
                .Property(e => e.CompanyRate)
                .HasPrecision(18, 6);

            modelBuilder.Entity<ConsultantPlacement>()
                .Property(e => e.VendorRate)
                .HasPrecision(18, 6);

            modelBuilder.Entity<PettyCash>()
                .Property(e => e.TransactionType)
                .IsUnicode(false);

            modelBuilder.Entity<PettyCash>()
                .Property(e => e.Amount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CompanyCompliance>()
               .Property(e => e.Amount)
               .HasPrecision(18, 6);


            modelBuilder.Entity<UserExpense>()
                .Property(e => e.Monthly_Salary)
                .HasPrecision(18, 6);

            modelBuilder.Entity<UserExpense>()
               .Property(e => e.Monthly_Salary2)
               .HasPrecision(18, 6);
            modelBuilder.Entity<UserExpense>()
              .Property(e => e.EOBI_Employer)
              .HasPrecision(18, 6);
            modelBuilder.Entity<UserExpense>()
           .Property(e => e.PF_Employer)
           .HasPrecision(18, 6);
            modelBuilder.Entity<UserExpense>()
         .Property(e => e.Mobile_Allowance)
         .HasPrecision(18, 6);
            modelBuilder.Entity<UserExpense>()
       .Property(e => e.Bonus)
       .HasPrecision(18, 6);
            modelBuilder.Entity<UserExpense>()
            .Property(e => e.Meal_Reimbursement)
            .HasPrecision(18, 6);

            modelBuilder.Entity<UserExpense>()
            .Property(e => e.Transportation)
            .HasPrecision(18, 6);

            modelBuilder.Entity<UserExpense>()
          .Property(e => e.Leave_Encashment)
          .HasPrecision(18, 6);

            modelBuilder.Entity<UserExpense>()
         .Property(e => e.Incentive_PSM)
         .HasPrecision(18, 6);

            modelBuilder.Entity<UserExpense>()
          .Property(e => e.Health_Insurance)
          .HasPrecision(18, 6);

            modelBuilder.Entity<UserExpense>()
     .Property(e => e.Medical_OPD)
     .HasPrecision(18, 6);

            modelBuilder.Entity<UserExpense>()
    .Property(e => e.Billable_Salary_PKR)
    .HasPrecision(18, 6);

            modelBuilder.Entity<UserExpense>()
 .Property(e => e.Billable_Salary_USD)
 .HasPrecision(18, 6);



            modelBuilder.Entity<UserCard>()
                    .HasMany(e => e.UserCardExpenses)
                    .WithRequired(e => e.UserCard)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserCardExpense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Requisition>()
             .Property(e => e.ProjectedRevenue)
             .HasPrecision(18, 6);

            modelBuilder.Entity<Requisition>()
           .Property(e => e.AllocatedBudget)
           .HasPrecision(18, 6);

            modelBuilder.Entity<UserAllocation>()
           .Property(e => e.Percentage)
           .HasPrecision(18, 6);

                modelBuilder.Entity<VendorConsultant>()
                .HasMany(e => e.ConsultantPlacements)
                .WithRequired(e => e.VendorConsultant)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<UserAllocationBilling>()
               .Property(e => e.AmountPKR)
               .HasPrecision(18, 6);

            modelBuilder.Entity<UserAllocationBilling>()
               .Property(e => e.AmountUSD)
               .HasPrecision(18, 6);

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
