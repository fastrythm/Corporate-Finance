using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace CorporateAndFinance.Core.Model
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser  
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public override string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string EmployeeNumber { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        public string Designation { get; set;}
 
        public bool IsDeleted { get; set; }

        public long? RequisitionID { get; set; }

        public virtual ICollection<UserCard> UserCards { get; set; }
 
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
 
        public virtual ICollection<UserDepartment> UserDepartments { get; set; }

      //  public virtual ICollection<UserInRole> UserInRoles { get; set; }
 
        public virtual ICollection<VendorConsultant> VendorConsultants { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }

        public virtual ICollection<UserExpense> UserExpenses { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CorporateFinanceConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
        }
   
 
 

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}