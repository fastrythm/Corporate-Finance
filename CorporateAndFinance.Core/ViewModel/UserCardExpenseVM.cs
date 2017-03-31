using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserCardExpenseVM
    {
        public long UserCardExpenseID { get; set; }

        public long UserCardID { get; set; }

        public string CardHolderName { get; set; }
        public string UserCardNumber { get; set; }

        public DateTime Date { get; set; }
      
        public string RecieptNumber { get; set; }

       
        public string Description { get; set; }

       
        public string BillType { get; set; }

        public decimal Amount { get; set; }

        public bool IsSalesRelated { get; set; }

        
        public string TransactionType { get; set; }

        
        public string Remarks { get; set; }

         
        public string CompanyReferenceType { get; set; }

        public Guid? CompanyReferenceID { get; set; }

        public string CompanyName { get; set; }

        public string ExpenseHead { get; set; }

        public Guid? ClientID { get; set; }
        public string ClientName { get; set; }

        public Guid? ConsultantID { get; set; }
        public string ConsultantName { get; set; }

        [NotMapped]
        public DataTablesViewModel DTObject { get; set; } = new DataTablesViewModel();
    }
}
