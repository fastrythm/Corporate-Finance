namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBankPosition")]
    public partial class CompanyBankPosition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CompanyBankPositionID { get; set; }

        public long CompanyBankID { get; set; }

        public DateTime Date { get; set; }


        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [NotMapped]
        public string AccountNumber { get; set; }
        public virtual CompanyBank CompanyBank { get; set; }
    }
}
