namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AddressID { get; set; }

        public Guid ReferenceID { get; set; }

        public long CodeID { get; set; }

        [Column("Address")]
        [Required]
        [StringLength(128)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Landline { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(20)]
        public string ZipCode { get; set; }

        public bool IsDeleted { get; set; }

        public int SortOrder { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }
    }
}
