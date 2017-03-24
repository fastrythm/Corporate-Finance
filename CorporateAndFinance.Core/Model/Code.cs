namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Code")]
    public partial class Code
    {
 
        public Code()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CodeID { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Type { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreatedBy { get; set; }

        public DateTime LastModified { get; set; }

    
    }
}
