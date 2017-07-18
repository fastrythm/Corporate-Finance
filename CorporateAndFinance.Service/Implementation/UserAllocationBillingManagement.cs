using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositories;
using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Implementation
{
    public class UserAllocationBillingManagement : IUserAllocationBillingManagement
    {
        private readonly IUserAllocationBillingRepository userAllocationBillingRepository;
        private readonly IUnitOfWork unitOfWork;


        public UserAllocationBillingManagement(IUserAllocationBillingRepository userAllocationBillingRepository, IUnitOfWork unitOfWork)
        {
            this.userAllocationBillingRepository = userAllocationBillingRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UserAllocationBilling> GetAllUserAllocationBillingByParam(UserAllocationBilling param, DateTime fromDate, DateTime toDate)
        {
            return userAllocationBillingRepository.GetAllUserAllocationBillingByParam(param, fromDate, toDate);
        }

        public void SaveUserAllocationBilling()
        {
            unitOfWork.Commit();
        }


        public UserAllocationBilling GetUserAllocationBilling(long id)
        {
            return userAllocationBillingRepository.GetById(id);
        }

        public bool Delete(UserAllocationBilling model)
        {
            model.LastModified = DateTime.Now;
            return userAllocationBillingRepository.Update(model);
        }

        public bool Add(UserAllocationBilling model)
        {
            return userAllocationBillingRepository.Add(model);
        }

        public bool Update(UserAllocationBilling model)
        {
            return userAllocationBillingRepository.Update(model);
        }

        public IEnumerable<UserAllocationBilling> GetAllUserAllocationBillingByDate(DateTime expenseDate)
        {
            return userAllocationBillingRepository.GetMany(x => x.BillingDate.Month == expenseDate.Month && x.BillingDate.Year == expenseDate.Year);
        }

        public void DeleteAllByDate(DateTime expenseDate)
        {
            userAllocationBillingRepository.Delete(x => x.BillingDate.Month == expenseDate.Month && x.BillingDate.Year == expenseDate.Year);
        }

       
     
    }
}
