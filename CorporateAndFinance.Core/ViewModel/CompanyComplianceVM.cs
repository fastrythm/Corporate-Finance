namespace CorporateAndFinance.Core.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class CompanyComplianceVM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CompanyComplianceID { get; set; }

        public Guid CompanyID { get; set; }

        [StringLength(128)]
        public string LegalAuthorityName { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [StringLength(512)]
        public string GeneralRemarks { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(512)]
        public string Remarks1 { get; set; }

        public Guid? Remarks1UserID { get; set; }

        public Guid? Remarks1UserName { get; set; }

        [StringLength(512)]
        public string Remarks2 { get; set; }

        public Guid? Remarks2UserID { get; set; }

        public Guid? Remarks2UserName { get; set; }

        public Decimal? Amount { get; set; }

        [StringLength(128)]
        public string FilePath { get; set; }

        public virtual string CompanyName { get; set; }

        [NotMapped]
        public DataTablesViewModel DTObject { get; set; }

        public CompanyComplianceVM()
        {
            DTObject = new DataTablesViewModel();
        }
    }
}
