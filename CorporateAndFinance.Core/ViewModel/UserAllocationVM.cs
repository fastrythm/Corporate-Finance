using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserAllocationVM
    {
        public long UserAllocationID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
        [DisplayName("Departments")]
        public long DepartmentID { get; set; }
        public long? RequisitionID { get; set; }
        public decimal Percentage { get; set; }
        public string RequestType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public DataTablesViewModel DTObject { get; set; }
        public long RequestedDepartmentID { get; set; }
        public string RequestedDepartmentName { get; set; }

        public Guid CreatedBy { get; set; }

        public string GroupNumber { get; set; }
        public string FormatedUserAllocationID { get; set; }
       
        public UserAllocationVM()
        {
            DTObject = new DataTablesViewModel();
        }
    }
}
