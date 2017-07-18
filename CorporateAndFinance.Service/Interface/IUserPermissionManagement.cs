using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserPermissionManagement
    {
        IEnumerable<UserPermission> GetAllPermissionByUserId(string userId);
        void SaveUserPermissions();
        bool DeleteAllByUserId(string userId);
        bool Add(UserPermission model);

    }
}
