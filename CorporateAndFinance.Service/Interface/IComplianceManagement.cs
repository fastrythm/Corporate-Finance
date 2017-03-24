using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IComplianceManagement
    {
        IEnumerable<CompanyComplianceVM> GetAllCompliancesByParam(CompanyComplianceVM param,DateTime fromDate, DateTime toDate);
        void SaveCompliance();
        CompanyCompliance GetCompliance(long id);
        bool Delete(CompanyCompliance compliance);
        bool Add(CompanyCompliance model);
        bool Update(CompanyCompliance model);
    }
}
