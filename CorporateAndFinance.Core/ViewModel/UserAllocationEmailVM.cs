using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
     public class UserAllocationEmailVM
    {
        public string Comments { get; set; }
        public string Status { get; set; }
        public string StyleSheet { get; set; }
        public List<UserAllocationVM> UserAllocations { get; set; }
    }
}
