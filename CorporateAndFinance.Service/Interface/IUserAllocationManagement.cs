using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.ViewModel;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserAllocationManagement
    {
        IEnumerable<UserAllocationVM> GetAllUserAllocationByParam(UserAllocationVM requisition, DateTime frdate, DateTime tdate, IEnumerable<UserDepartment> userDepartments, bool isAdmin, string type);
        void SaveUserAllocation();
        UserAllocation GetUserAllocation(long id);

        IEnumerable<UserAllocation> GetUserAllocationsByRequisition(long reqId);
        bool Delete(UserAllocation model);
        bool Add(UserAllocation model);
        bool Update(UserAllocation model);
        IEnumerable<UserAllocation> GetUserAllocationsByUserIdDepartmentId(string userID, long departmentID);
    }
}
