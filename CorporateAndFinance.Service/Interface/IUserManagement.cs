using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserManagement
    {
        IEnumerable<ApplicationUser> GetAllUsers();

        IEnumerable<ApplicationUser> GetAllUsersByDepartments(IEnumerable<UserDepartment> departments);

        IEnumerable<ApplicationUser> GetAllUsersByRoleAndDepartment(string roleId , long departmentId);
    }
}
