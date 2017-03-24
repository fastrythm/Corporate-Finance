using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
