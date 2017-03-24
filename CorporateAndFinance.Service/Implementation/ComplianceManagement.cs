using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositories;
using CorporateAndFinance.Core.ViewModel;

namespace CorporateAndFinance.Service.Implementation
{
    public class ComplianceManagement : IComplianceManagement
    {
        private readonly IComplianceRepository complianceRepository;
        private readonly IUnitOfWork unitOfWork;


        public ComplianceManagement(IComplianceRepository complianceRepository, IUnitOfWork unitOfWork)
        {
            this.complianceRepository = complianceRepository;
            this.unitOfWork = unitOfWork;
        }
        public bool Add(CompanyCompliance model)
        {
            return complianceRepository.Add(model);
        }

        public bool Delete(CompanyCompliance compliance)
        {
          return complianceRepository.Delete(compliance);
        }

        public IEnumerable<CompanyComplianceVM> GetAllCompliancesByParam(CompanyComplianceVM param, DateTime fromDate, DateTime toDate)
        {
            return complianceRepository.GetAllCompliancesByParam(param, fromDate, toDate);
        }

        public CompanyCompliance GetCompliance(long id)
        {
            return complianceRepository.GetById(id);
        }

        public void SaveCompliance()
        {
            unitOfWork.Commit();
        }

        public bool Update(CompanyCompliance model)
        {
            return complianceRepository.Update(model);
        }
    }
}
