using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserAllocationBillingManagement
    {
        IEnumerable<UserAllocationBilling> GetAllUserAllocationBillingByParam(UserAllocationBilling param, DateTime fromDate,DateTime toDate);
        UserAllocationBilling GetUserAllocationBilling(long id);
        void SaveUserAllocationBilling();
        bool Delete(UserAllocationBilling model);
        bool Add(UserAllocationBilling model);
        bool Update(UserAllocationBilling model);
        IEnumerable<UserAllocationBilling> GetAllUserAllocationBillingByDate(DateTime expenseDate);
        void DeleteAllByDate(DateTime expenseDate);
        UserAllocationBilling GetUserAllocationBillingByExpenseId(Guid id);
    }
}
