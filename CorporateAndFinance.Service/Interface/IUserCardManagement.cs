using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserCardManagement
    {
        UserCard GetUserCard(long id);
        UserCard GetUserCardByAccountNumber(string number);
        void SaveUserCard();
        bool Delete(UserCard model);
        bool Add(UserCard model);
        bool Update(UserCard model);
        IEnumerable<UserCard> GetAllUserCards();
    }
}
