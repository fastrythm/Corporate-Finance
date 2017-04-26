using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class BankReconciliationQBWiseReport
    {
        public string Bank { get; set; }
        public string AccountNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
              ApplyFormatInEditMode = true)]
        public DateTime? TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal? Amount { get; set; }

        public string CategoryReference { get; set; }
        public decimal? QBClosingBalance { get; set; }
        public decimal? BankClosingBalance { get; set; }
        public string TransactionStatus { get; set; }
    }
}
