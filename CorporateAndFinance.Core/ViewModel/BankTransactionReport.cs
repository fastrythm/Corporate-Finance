using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
  public  class BankTransactionReport
    {
        public long CompanyBankID { get; set; }
        public string Company { get; set; }
        public string AccountNumber { get; set; }
        public string CategoryType { get; set; }
        public long? ToCompanyBankID { get; set; }
        public Guid? CategoryReferenceID { get; set; }

        public DateTime? TransactionDate { get; set; }
        public string TransactionType { get; set; }

        public decimal? Amount { get; set; }
        public string CategoryReference { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? ClosingBalance { get; set; }
        
    }
}
