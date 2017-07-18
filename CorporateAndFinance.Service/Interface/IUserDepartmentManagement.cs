using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserDepartmentManagement
    {
        bool Add(UserDepartment model);
        bool Update(UserDepartment model);
        void SaveUserDepartment();
        IEnumerable<UserDepartment> GetAllUserDepartmentById(string userId);
        IEnumerable<UserDepartmentVM> GetAllUserDepartmentByUserId(string userId,bool isAdmin);
        UserDepartment GetUserPrimaryDepartmentById(string userId);
 
    }
}
