using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;

namespace CorporateAndFinance.Service.Implementation
{
    public class DepartmentManagement : IDepartmentManagement
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IUnitOfWork unitOfWork;


        public DepartmentManagement(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            this.departmentRepository = departmentRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool Add(Department model)
        {
           return departmentRepository.Add(model);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return departmentRepository.GetMany(dept => dept.IsActive == true);
        }

        public Department GetDepartment(string name)
        {
            return departmentRepository.Get(dept => dept.IsActive == true && dept.Name.Equals(name));
        }

        public void SaveDepartment()
        {
            unitOfWork.Commit();
        }
    }
}
