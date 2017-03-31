using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserPermission
    {
        bool HavePermission(Guid UserId, long PermissionId);
    }
}
