using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserCardExpenseManagement
    {
        IEnumerable<UserCardExpenseVM> GetAllUserCardExpensesByParam(UserCardExpenseVM param, DateTime fromDate,DateTime toDate);
        UserCardExpense GetUserCardExpense(long id);
        void SaveUserCardExpense();
        bool Delete(UserCardExpense model);
        bool Add(UserCardExpense model);
        bool Update(UserCardExpense model);

    }
}
