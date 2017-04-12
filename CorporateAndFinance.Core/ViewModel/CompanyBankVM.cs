using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class CompanyBankVM
    {
        public long CompanyBankID { get; set; }

        public Guid CompanyID { get; set; }

        public long BankID { get; set; }

        public string CompanyName { get; set; }
        public string BankName { get; set; }

        public string AccountNumber { get; set; }
    }
}
