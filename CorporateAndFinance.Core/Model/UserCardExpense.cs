namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCardExpense")]
    public partial class UserCardExpense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserCardExpenseID { get; set; }

        public long UserCardID { get; set; }

        public DateTime Date { get; set; }

        [StringLength(50)]
        public string RecieptNumber { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string BillType { get; set; }

        public decimal Amount { get; set; }

        public bool IsSalesRelated { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; }

        [StringLength(512)]
        public string Remarks { get; set; }

        [StringLength(512)]
        public string PaymentDetail { get; set; }

        [StringLength(50)]
        public string ExpenseHead { get; set; }

        public Guid? ClientID { get; set; }

        public Guid? ConsultantID { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public virtual Consultant Consultant { get; set; }

        public virtual UserCard UserCard { get; set; }
    }
}
