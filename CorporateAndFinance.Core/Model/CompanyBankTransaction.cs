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
        [Required]
        [Display(Name = "Receipt Number")]
        public string ReceiptNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        [Required]
        public decimal Amount { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(128)]
        [Display(Name = "Category")]
        [Required]
        public string CategoryType { get; set; }

        [Display(Name = "To Account Number")]
        public long? ToCompanyBankID { get; set; }

        [Display(Name = "Category Reference")]
        public Guid? CategoryReferenceID { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int TransactionStatus { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public virtual CompanyBank CompanyBank { get; set; }

        [NotMapped]
        public IEnumerable<CompanyBankVM> CompanyBankAccounts { get; set; }

        [NotMapped]
        public IEnumerable<Company> CategoryVendors { get; set; }
        [NotMapped]
        public Guid? CategoryVendorID { get; set; }
        [NotMapped]
        public IEnumerable<Company> CategoryClients { get; set; }
        [NotMapped]
        public Guid? CategoryClientID { get; set; }
        [NotMapped]
        public IEnumerable<Consultant> CategoryConsultants { get; set; }

        [NotMapped]
        public Guid? CategoryConsultantID { get; set; }

        public Guid? ReferenceID { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}
