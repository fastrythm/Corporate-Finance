using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;

namespace CorporateAndFinance.Service.Implementation
{
    public class UserPermissionManagement : IUserPermissionManagement
    {
        private readonly IUserPermissionRepository userPermissionRepository;
        private readonly IUnitOfWork unitOfWork;


        public UserPermissionManagement(IUserPermissionRepository userPermissionRepository, IUnitOfWork unitOfWork)
        {
            this.userPermissionRepository = userPermissionRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UserPermission> GetAllPermissionByUserId(string userId)
        {
            return userPermissionRepository.GetMany(x => x.UserID == userId);
        }

        public void SaveUserPermissions()
        {
            unitOfWork.Commit();
        }

        public bool DeleteAllByUserId(string userId)
        {
           return userPermissionRepository.Delete(x => x.UserID == userId);
        }

        public bool Add(UserPermission userPermissions)
        {
            return userPermissionRepository.Add(userPermissions);
        }

       


    }
}
