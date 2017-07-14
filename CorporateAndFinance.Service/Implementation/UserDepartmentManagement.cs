using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Core.ViewModel;

namespace CorporateAndFinance.Service.Implementation
{
    public class UserDepartmentManagement : IUserDepartmentManagement
    {
        private readonly IUserDepartmentRepository userdepartmentRepository;
        private readonly IUnitOfWork unitOfWork;


        public UserDepartmentManagement(IUserDepartmentRepository userdepartmentRepository, IUnitOfWork unitOfWork)
        {
            this.userdepartmentRepository = userdepartmentRepository;
            this.unitOfWork = unitOfWork;
        }



        public void SaveUserDepartment()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<UserDepartment> GetAllUserDepartmentById(string userId)
        {
            return userdepartmentRepository.GetMany(x =>  x.UserID == userId && x.IsActive );
        }

        public UserDepartment GetUserPrimaryDepartmentById(string userId)
        {
            return userdepartmentRepository.Get(x => x.UserID == userId && x.IsActive && x.IsPrimary);
        }

        public IEnumerable<UserDepartmentVM> GetAllUserDepartmentByUserId(string userId,bool isAdmin)
        {
            return userdepartmentRepository.GetAllUserDepartmentByUserId(userId, isAdmin);
        }

        public bool Add(UserDepartment model)
        {
            return userdepartmentRepository.Add(model);
        }

        public bool Update(UserDepartment model)
        {
            return userdepartmentRepository.Update(model);
        }


    }
}
