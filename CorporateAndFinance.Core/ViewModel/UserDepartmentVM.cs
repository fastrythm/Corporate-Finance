using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserDepartmentVM
    {
        public long UserDepartmentID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Department")]
        public long DepartmentID { get; set; }
        public string RoleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrimary { get; set; }
        public string DepartmentName{get;set;}
    }
}
