using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class InterCompanyReconciliationReport
    {
        public string ShortName { get; set; }
        public string TransactionType { get; set; }

        public decimal? Amount { get; set; }
        public string ToComapnyShortName { get; set; }
    }
}
