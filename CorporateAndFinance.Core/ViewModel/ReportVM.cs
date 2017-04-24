using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class ReportVM
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string FromDate { get; set; } = DateTime.Now.ToShortDateString();
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
              ApplyFormatInEditMode = true)]
        public string ToDate { get; set; } = DateTime.Now.ToShortDateString();
    }
}
