namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VendorConsultant")]
    public partial class VendorConsultant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VendorConsultant()
        {
            ConsultantPlacements = new HashSet<ConsultantPlacement>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long VendorConsultantID { get; set; }

        public Guid UserID { get; set; }

        public Guid ConsultantID { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public virtual Consultant Consultant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsultantPlacement> ConsultantPlacements { get; set; }

         public virtual ApplicationUser User { get; set; }
    }
}
