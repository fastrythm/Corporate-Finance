namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ViewModel;

    [Table("CompanyBankTransaction")]
    public partial class CompanyBankTransaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CompanyBankTransactionID { get; set; }

        [Display(Name = "Account Number")]
        public long CompanyBankID { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [StringLength(50)]
        [Display(Name = "Receipt Number")]
        public string ReceiptNumber { get; set; }

        public decimal Amount { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(128)]
        [Display(Name = "Category")]
        public string CategoryType { get; set; }

        [Display(Name = "To Account Number")]
        public long? ToCompanyBankID { get; set; }

        [Display(Name = "Status")]
        public int TransactionStatus { get; set; }

        public bool IsDeleted { get; set; }
  
        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public virtual CompanyBank CompanyBank { get; set; }

        [NotMapped]
        public IEnumerable<CompanyBankVM> CompanyBankAccounts { get; set; }

    }
}
