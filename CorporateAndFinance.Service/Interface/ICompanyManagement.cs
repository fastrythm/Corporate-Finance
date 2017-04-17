using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface ICompanyManagement
    {
        IEnumerable<Company> GetAllCompanies();
        IEnumerable<Company> GetAllVendorCompanies();
        IEnumerable<Company> GetAllClientCompanies();
        Company GetCompanyByNumber(long number);
        IEnumerable<CompanyBankVM> GetCompanyBankAccounts();
        bool Add(Company model);
        void SaveCompany();
    }
}
