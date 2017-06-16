using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IRequisitionManagement
    {
        IEnumerable<RequisitionVM> GetAllRequisitionByParam(RequisitionVM param, DateTime fromDate,DateTime toDate, IEnumerable<UserDepartment> departments, bool isAdmin);
        void SaveRequisition();
        Requisition GetRequisition(long id);
        bool Delete(Requisition model);
        bool Add(Requisition model);
        bool Update(Requisition model);
        bool DeAttach(Requisition model);
    }
}
