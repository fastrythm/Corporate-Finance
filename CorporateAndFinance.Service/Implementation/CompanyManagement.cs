using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Core.Helper.Structure;

namespace CorporateAndFinance.Service.Implementation
{
    public class CompanyManagement:ICompanyManagement
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IUnitOfWork unitOfWork;


        public CompanyManagement(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            this.companyRepository = companyRepository;
            this.unitOfWork = unitOfWork;
        }
        public  IEnumerable<Company> GetAllCompanies()
        {
            return companyRepository.GetMany(comp => comp.IsActive == true);
        }

        public Company GetCompanyByNumber(long number)
        {
            return companyRepository.Get(x => x.CompanyNumber == number);
        }

        public void SaveCompany()
        {
            unitOfWork.Commit();
        }

        public bool Add(Company model)
        {
            return companyRepository.Add(model);
        }

        public IEnumerable<CompanyBankVM> GetCompanyBankAccounts()
        {
            return companyRepository.GetCompanyBankAccounts();
        }

        public IEnumerable<Company> GetAllVendorCompanies()
        {
            return companyRepository.GetMany(comp => comp.IsActive == true && comp.CompanyType == CompanyType.Vendor);
        }

        public IEnumerable<Company> GetAllClientCompanies()
        {
            return companyRepository.GetMany(comp => comp.IsActive == true && comp.CompanyType == CompanyType.Client);
        }
    }
}
