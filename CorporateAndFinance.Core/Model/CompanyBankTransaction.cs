namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBankTransaction")]
    public partial class CompanyBankTransaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CompanyBankTransactionID { get; set; }

        public long CompanyBankID { get; set; }

        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentType { get; set; }

        [StringLength(50)]
        public string ReceiptNumber { get; set; }

        public decimal Amount { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(128)]
        public string CategoryType { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public virtual CompanyBank CompanyBank { get; set; }
    }
}
