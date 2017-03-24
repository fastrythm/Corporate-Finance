namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConsultantDocument")]
    public partial class ConsultantDocument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ConsultantDocumentID { get; set; }

        public Guid ConsultantID { get; set; }

        public long CodeID { get; set; }

        [Required]
        [StringLength(128)]
        public string DocumentName { get; set; }

        [StringLength(128)]
        public string DocumentSystemName { get; set; }

        public int SortOrder { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        [NotMapped]
        public  Code Code { get; set; }

        public virtual Consultant Consultant { get; set; }
    }
}
