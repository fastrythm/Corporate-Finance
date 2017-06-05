using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Model
{
    public class UserExpense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserExpenseID { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser ExpenseUser { get; set; }
        public long DepartmentID { get; set; }
        public DateTime ExpenseDate { get; set; }
    
        public decimal Monthly_Salary { get; set; }
        public decimal Monthly_Salary2 { get; set; }
        public decimal EOBI_Employer { get; set; }
        public decimal PF_Employer { get; set; }
        public decimal Mobile_Allowance { get; set; }
        public decimal Bonus { get; set; }
        public decimal Meal_Reimbursement { get; set; }
        public decimal Transportation { get; set; }
        public decimal Leave_Encashment { get; set; }
        public decimal Incentive_PSM { get; set; }
        public decimal Health_Insurance { get; set; }
        public decimal Medical_OPD { get; set; }
        public decimal Billable_Salary_PKR { get; set; }
        public decimal Billable_Salary_USD { get; set; }


        [DefaultValue("True")]
        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Department Department { get; set; }
       
    }
}
