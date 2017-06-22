using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IRequisitionApprovalManagement
    {
        IEnumerable<RequisitionApproval> GetAllRequisitionApprovalByRequisition(long id);
        void SaveRequisitionApproval();
        RequisitionApproval GetRequisitionApproval(long id);
        bool Delete(RequisitionApproval model);
        bool Add(RequisitionApproval model);
        bool Update(RequisitionApproval model);
        RequisitionApproval GetRequisitionApprovalByDeptIdAndRequisition(long departmentID, long requisitionID);
    }
}
