using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserTaskManagement
    {
        IEnumerable<UserTaskVM> GetTaskByCriteria(UserTaskVM param,string userId);
        void SaveUserTask();
        bool Delete(UserTask model);
        bool Add(UserTask model);
        bool Update(UserTask model);
        UserTask GetUserTaskById(long id);
        UserTaskEmailVM GetUserTaskEmailDetails(long taskId);
    }
}
