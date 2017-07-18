using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface ISLAApprovalManagement
    {
        IEnumerable<SLAApproval> GetAllSLAApprovalByType(string Type);
        bool HaveSLALevelRightByType(IEnumerable<UserDepartmentVM> userDepartments, string slaType);


    }
}
