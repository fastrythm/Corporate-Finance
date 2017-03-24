namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCompany")]
    public partial class UserCompany
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserCompanyID { get; set; }

        public Guid UserID { get; set; }

        public Guid CompanyID { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public virtual Company Company { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
