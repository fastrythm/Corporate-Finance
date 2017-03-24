using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorporateAndFinance.Core.Model
{
    [Table("BankBranch")]
    public class BankBranch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BankBranchID { get; set; }
        public long BankID { get; set; }
        public string BranchName { get; set; }
        [StringLength(50)]
        public string BranchCode { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string ContactPerson { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }


        [StringLength(128)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

       
    }
}
