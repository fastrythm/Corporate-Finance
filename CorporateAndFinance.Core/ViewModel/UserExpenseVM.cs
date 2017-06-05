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
        public long UserExpenseID { get; set; }

        public long UserID { get; set; }

        public string UserName { get; set; }
        public string UserNumber { get; set; }
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal Billable_Salary_PKR { get; set; }
        public decimal Billable_Salary_USD { get; set; }
        public DateTime ExpenseDate { get; set; }
      

        [NotMapped]
        public DataTablesViewModel DTObject { get; set; } = new DataTablesViewModel();
    }
}
