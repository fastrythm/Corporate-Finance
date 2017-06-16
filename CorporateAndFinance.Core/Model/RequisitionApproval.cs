using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Model
{
    [Table("RequisitionApproval")]
    public class RequisitionApproval
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RequisitionApprovalID { get; set; }
        public long RequisitionID { get; set; }
        public long DepartmentID { get; set; }
        [DefaultValue("True")]
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public virtual Department Department { get; set; }
        public virtual Requisition Requisition { get; set; }
    }
}
