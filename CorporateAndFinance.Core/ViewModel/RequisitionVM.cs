using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class RequisitionVM
    {
        public long RequisitionID { get; set; }
        public long DepartmentID { get; set; }
        public string JobTitle { get; set; }
        public string GradeLevel { get; set; }
        public string Status { get; set; }
        public int NoOfPosition { get; set; }
        public DateTime RequisitionDate { get; set; }
      
        public DataTablesViewModel DTObject { get; set; }

        public RequisitionVM()
        {
            DTObject = new DataTablesViewModel();
        }
    }
}
