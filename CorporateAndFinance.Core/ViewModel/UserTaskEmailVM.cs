using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserTaskEmailVM
    {
        public string TicketNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
             ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public List<UserTaskDetailVM> UserTaskDetails { get; set; } = new List<UserTaskDetailVM>();
    }
}
