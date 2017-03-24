namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConsultantPlacement")]
    public partial class ConsultantPlacement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ConsultantPlacementID { get; set; }

        public long VendorConsultantID { get; set; }

        public long CodeID { get; set; }

        public Guid CompanyID { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(25)]
        public string RateType { get; set; }

        public decimal ConsultantRate { get; set; }

        public decimal CompanyRate { get; set; }

        public decimal VendorRate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }
        [NotMapped]
        public  Code Code { get; set; }
        [NotMapped]
        public  Company Company { get; set; }

        public virtual VendorConsultant VendorConsultant { get; set; }
    }
}
