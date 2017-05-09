namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;
    using ViewModel;

    [Table("CompanyCompliance")]
    public partial class CompanyCompliance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CompanyComplianceID { get; set; }

        [Display(Name = "Company")]
        public Guid CompanyID { get; set; }

        [StringLength(128), Display(Name = "Legal Authority Name")]
        public string LegalAuthorityName { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } =  DateTime.Now ;

        [StringLength(512), Display(Name = "General Remarks")]
        public string GeneralRemarks { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(512)]
        public string Remarks1 { get; set; }

        public Guid? Remarks1UserID { get; set; }

        [StringLength(512)]
        public string Remarks2 { get; set; }

        public Guid? Remarks2UserID { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.###}", ApplyFormatInEditMode = true)]
        public Decimal? Amount { get; set; }

        [StringLength(128), Display(Name = "File")]
        public string FilePath { get; set; }

        public virtual Company Company { get; set; }

        public IEnumerable<Company> Companies { get; set; }



    }
}
