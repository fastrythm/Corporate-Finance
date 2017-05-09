using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserTaskDetailVM
    {
        public string FromUser { get; set; }
        public string FromUserEmail { get; set; }
        public string ToUser { get; set; }
        public string ToUserEmail { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
