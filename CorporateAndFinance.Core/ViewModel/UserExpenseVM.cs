using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserExpenseVM
    {
        public Guid UserExpenseID { get; set; }

        public string SerialNumber { get; set; }

        public long UserID { get; set; }

        public string UserName { get; set; }
        public string UserNumber { get; set; }
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
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
        public DateTime ExpenseDate { get; set; }
      

        [NotMapped]
        public DataTablesViewModel DTObject { get; set; } = new DataTablesViewModel();
    }
}
