using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserAllocationManagement
    {
        IEnumerable<UserAllocation> GetAllUserAllocationByParam(DateTime fromDate,DateTime toDate);
        void SaveUserAllocation();
        UserAllocation GetUserAllocation(long id);

        IEnumerable<UserAllocation> GetUserAllocationsByRequisition(long reqId);
        bool Delete(UserAllocation model);
        bool Add(UserAllocation model);
        bool Update(UserAllocation model);
    }
}
