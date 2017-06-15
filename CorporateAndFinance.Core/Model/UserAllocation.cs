using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Model
{
    [Table("UserAllocation")]
    public class UserAllocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserAllocationID { get; set; }
        public string UserID { get; set; }
        public long DepartmentID { get; set; }
        public long? RequisitionID { get; set; }
        public decimal Percentage { get; set; }
        [DefaultValue("True")]
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public virtual Department Department { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Requisition Requisition { get; set; }
    }
}
