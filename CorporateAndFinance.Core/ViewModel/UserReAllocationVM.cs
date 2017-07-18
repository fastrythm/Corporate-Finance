using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserReAllocationVM
    {
        [DisplayName("Users")]
        public string UserId { get; set; }
        public string UserPrimaryDepartment { get; set; }
     
        public IEnumerable<ApplicationUser> UserList { get; set; }
        public long[] SelectedDepartment { get; set; }
        public decimal[] SelectedDepartmentPercentage { get; set; }
        public IEnumerable<UserAllocation> UserAllocatedDepartments { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
