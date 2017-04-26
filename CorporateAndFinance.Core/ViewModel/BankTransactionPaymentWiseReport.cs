using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class BankTransactionPaymentWiseReport
    {
        public DateTime TransactionDate { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; }
        public string ReceiptNumber { get; set; }
        public string PaymentType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
