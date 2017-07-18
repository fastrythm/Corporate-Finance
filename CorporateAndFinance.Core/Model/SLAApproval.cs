using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Model
{
    [Table("SLAApproval")]
    public class SLAApproval
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SLAApprovalID { get; set; }
        public long DepartmentID { get; set; }
        public string UserID { get; set; }
        public string SLAType { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }

    }
}
