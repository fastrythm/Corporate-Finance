using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserTaskVM
    {
        public long UserTaskID { get; set; }
      
        public string Title { get; set; }
       
        public string Description { get; set; }
       
        public string Type { get; set; }
        public int Priority { get; set; }
     
        public DateTime DueDate { get; set; }

        public string FromUser { get; set; }
        public string ToUser { get; set; }

        public string Remarks { get; set; }
 
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DataTablesViewModel DTObject { get; set; }

        public UserTaskVM()
        {
            DTObject = new DataTablesViewModel();
        }
    }
}
