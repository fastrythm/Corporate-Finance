namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ViewModel;

    [Table("PettyCash")]
    public partial class PettyCash
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PettyCashID { get; set; }

        [StringLength(128)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public bool IsDeleted { get; set; }

        public Guid UserID { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        [NotMapped]
        public DataTablesViewModel DTObject { get; set; }
     
        public PettyCash()
        {
            DTObject = new DataTablesViewModel();
        }
    }
}
