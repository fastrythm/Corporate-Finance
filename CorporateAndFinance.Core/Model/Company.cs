namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            CompanyBanks = new HashSet<CompanyBank>();
            CompanyCompliances = new HashSet<CompanyCompliance>();
            ConsultantPlacements = new HashSet<ConsultantPlacement>();
            UserCardExpenses = new HashSet<UserCardExpense>();
            UserCompanies = new HashSet<UserCompany>();
        }

        public Guid CompanyID { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public long CodeID { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public Code Code { get; set; }
 
        public virtual ICollection<CompanyBank> CompanyBanks { get; set; }
 
        public virtual ICollection<CompanyCompliance> CompanyCompliances { get; set; }

        public virtual ICollection<ConsultantPlacement> ConsultantPlacements { get; set; }
 
        public virtual ICollection<UserCardExpense> UserCardExpenses { get; set; }

 
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
    }
}
