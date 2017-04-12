using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;

namespace CorporateAndFinance.Service.Interface
{
    public interface IBankTransactionManagement
    {
        IEnumerable<CompanyBankTransactionVM> GetAllBankTransactionByParam(CompanyBankTransactionVM transaction, DateTime frdate, DateTime tdate);
        CompanyBankTransaction GetCompanyBankTransaction(long id);
        bool Delete(CompanyBankTransaction transaction);
        void SaveCompanyBankTransaction();
        bool Add(CompanyBankTransaction model);
        bool Update(CompanyBankTransaction model);
    }
}
