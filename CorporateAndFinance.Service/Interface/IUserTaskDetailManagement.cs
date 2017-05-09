using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserTaskDetailManagement
    {
        IEnumerable<UserTaskDetail> GetAllTaskDetailsByTaskId(long taskId);
        UserTaskDetail GetActiveTaskDetailsByTaskId(long taskId);
        void SaveUserTaskDetail();
        bool Add(UserTaskDetail model);
        bool Delete(long taskId);
    }
}
