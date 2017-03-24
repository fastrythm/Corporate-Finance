namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consultant")]
    public partial class Consultant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consultant()
        {
            ConsultantDocuments = new HashSet<ConsultantDocument>();
            UserCardExpenses = new HashSet<UserCardExpense>();
            VendorConsultants = new HashSet<VendorConsultant>();
        }

        public Guid ConsultantID { get; set; }

        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

 
        public virtual ICollection<ConsultantDocument> ConsultantDocuments { get; set; }
 
        public virtual ICollection<UserCardExpense> UserCardExpenses { get; set; }
 
        public virtual ICollection<VendorConsultant> VendorConsultants { get; set; }
    }
}
