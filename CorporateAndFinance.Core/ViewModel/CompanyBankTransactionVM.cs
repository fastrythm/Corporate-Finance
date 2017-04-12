using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class CompanyBankTransactionVM
    {
        public long CompanyBankTransactionID { get; set; }

        public long CompanyBankID { get; set; }

        public string CompanyName { get; set; }
        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentType { get; set; }

        [StringLength(50)]
        public string ReceiptNumber { get; set; }

        public decimal Amount { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(128)]
        public string CategoryType { get; set; }

        public long? ToCompanyBankID { get; set; }


        public string ToCompanyName { get; set; }
        public string ToBankName { get; set; }

        public string ToAccountNumber { get; set; }

        public bool IsDeleted { get; set; }

        public int TransactionStatus { get; set; }

        public DataTablesViewModel DTObject { get; set; }

        public CompanyBankTransactionVM()
        {
            DTObject = new DataTablesViewModel();
        }

    }
}
