using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IDepartmentManagement
    {
        IEnumerable<Department> GetAllDepartments();
        bool Add(Department model);
        void SaveDepartment();
        Department GetDepartment(string name);
    }
}
