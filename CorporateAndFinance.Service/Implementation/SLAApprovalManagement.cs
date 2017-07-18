using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Core.ViewModel;

namespace CorporateAndFinance.Service.Implementation
{
    public class SLAApprovalManagement : ISLAApprovalManagement
    {
        private readonly ISLAApprovalRepository slaApprovalRepository;
        private readonly IUnitOfWork unitOfWork;

        public SLAApprovalManagement(ISLAApprovalRepository slaApprovalRepository, IUnitOfWork unitOfWork)
        {
            this.slaApprovalRepository = slaApprovalRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<SLAApproval> GetAllSLAApprovalByType(string slaType)
        {
            return slaApprovalRepository.GetMany(x => x.SLAType.Equals(slaType));
        }

        public bool HaveSLALevelRightByType(IEnumerable<UserDepartmentVM> userDepartments, string slaType)
        {
            var sla = slaApprovalRepository.GetMany(x => x.SLAType.Equals(slaType));

            if (sla != null && sla.Count() > 0)
            {
                foreach (var department in userDepartments)
                {
                   var found =  sla.Where(x => x.DepartmentID == department.DepartmentID).SingleOrDefault();
                    if (found != null)
                    {
                        return true;
                    }
                 }
            }
            return false;
        }
    }
}
