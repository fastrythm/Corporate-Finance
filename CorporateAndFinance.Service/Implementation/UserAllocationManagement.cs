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
using CorporateAndFinance.Core.Helper.Structure;

namespace CorporateAndFinance.Service.Implementation
{
    public class UserAllocationManagement : IUserAllocationManagement
    {
        private readonly IUserAllocationRepository userAllocationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserAllocationManagement(IUserAllocationRepository userAllocationRepository, IUnitOfWork unitOfWork)
        {
            this.userAllocationRepository = userAllocationRepository;
            this.unitOfWork = unitOfWork;
        }
        public bool Add(UserAllocation model)
        {
            return userAllocationRepository.Add(model);
        }

        public bool Delete(UserAllocation model)
        {
            return userAllocationRepository.Delete(model);
        }

        public IEnumerable<UserAllocationVM> GetAllUserAllocationByParam(UserAllocationVM requisition, DateTime frdate, DateTime tdate, IEnumerable<UserDepartment> userDepartments, bool isAdmin, string type, string allocationType)
        {
            return userAllocationRepository.GetAllUserAllocationByParam(requisition,frdate, tdate, userDepartments, isAdmin, type, allocationType);
        }

        public UserAllocation GetUserAllocation(long id)
        {
           return userAllocationRepository.GetById(id);
        }

        public void SaveUserAllocation()
        {
            unitOfWork.Commit();
        }

        public bool Update(UserAllocation model)
        {
            return userAllocationRepository.Update(model);
        }

        public IEnumerable<UserAllocation> GetUserAllocationsByRequisition(long reqId)
        {
            return userAllocationRepository.GetMany(x=>x.RequisitionID == reqId && !x.Status.Equals(RequestStatus.Deleted));
        }

        public IEnumerable<UserAllocation> GetUserAllocationsByUserIdDepartmentId(string userID, long departmentID)
        {
            return userAllocationRepository.GetMany(x => x.IsActive && x.UserID == userID && x.DepartmentID == departmentID && !x.Status.Equals(RequestStatus.Deleted));
        }

        public IEnumerable<UserAllocation> GetUserAllocationsByUserId(string userId)
        {
            return userAllocationRepository.GetMany(x => x.IsActive && x.UserID == userId && x.Status.Equals(RequestStatus.Approved));
        }

        public IEnumerable<UserAllocation> GetUserPendingAllocationsByUserId(string userId)
        {
            return userAllocationRepository.GetMany(x => !x.IsActive && x.UserID == userId && x.Status.Equals(RequestStatus.Pending));
        }

        public IEnumerable<UserAllocation> GetUserAllocationByGroupNumber(long groupNumber)
        {
            return userAllocationRepository.GetMany(x => x.GroupNumber == groupNumber);
        }

        public IEnumerable<UserAllocation> GetActiveUserAllocationExceptGroupNumber(string userID, long groupNumber)
        {
            return userAllocationRepository.GetMany(x => x.IsActive && x.UserID ==  userID && x.GroupNumber != groupNumber && x.Status.Equals(RequestStatus.Approved));
        }
    }
}
