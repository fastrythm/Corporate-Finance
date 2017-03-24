namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBank")]
    public partial class CompanyBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompanyBank()
        {
            CompanyBankPositions = new HashSet<CompanyBankPosition>();
            CompanyBankTransactions = new HashSet<CompanyBankTransaction>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CompanyBankID { get; set; }

        public Guid CompanyID { get; set; }

        public long BankID { get; set; }

        public long BankBranchID { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        public string AccountType { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyBankPosition> CompanyBankPositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyBankTransaction> CompanyBankTransactions { get; set; }
    }
}
