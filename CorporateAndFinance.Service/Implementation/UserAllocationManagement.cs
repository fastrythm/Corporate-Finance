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

        public IEnumerable<UserAllocation> GetAllUserAllocationByParam(DateTime fromDate, DateTime toDate)
        {
            return userAllocationRepository.GetAllUserAllocationByParam(fromDate, toDate);
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
            return userAllocationRepository.GetMany(x=>x.IsActive && x.RequisitionID == reqId);
        }
    }
}
