using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Model
{
    [Table("UserAllocationBilling")]
    public class UserAllocationBilling
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserAllocationBillingID { get; set; }
        public Guid UserExpenseID { get; set; }
        public string UserID { get; set; }
        public long DepartmentID { get; set; }
        public DateTime BillingDate { get; set; }
        public decimal Percentage { get; set; }
        public decimal AmountPKR { get; set; }
        public decimal AmountUSD { get; set; }

        public bool IsDeleted { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Department Department { get; set; }
        public virtual UserExpense UserExpense { get; set; }
    }
}
