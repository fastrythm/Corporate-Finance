using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class CompanyBankPositionVM
    {
        public long? CompanyBankPositionID { get; set; }

        public long CompanyBankID { get; set; }

        public Guid CompanyID { get; set; }
        public string CompanyName { get; set; }
       
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public DateTime? PositionDate { get; set; }
        public Decimal? Balance { get; set; }
    }
}
