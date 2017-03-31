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
    public class UserCardExpenseManagement : IUserCardExpenseManagement
    {
        private readonly IUserCardExpenseRepository userCardExpenseRepository;
        private readonly IUnitOfWork unitOfWork;


        public UserCardExpenseManagement(IUserCardExpenseRepository userCardExpenseRepository, IUnitOfWork unitOfWork)
        {
            this.userCardExpenseRepository = userCardExpenseRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UserCardExpenseVM> GetAllUserCardExpensesByParam(UserCardExpenseVM param, DateTime fromDate, DateTime toDate)
        {
            return userCardExpenseRepository.GetAllUserCardExpensesByParam(param, fromDate, toDate);
        }

        public void SaveUserCardExpense()
        {
            unitOfWork.Commit();
        }


        public UserCardExpense GetUserCardExpense(long id)
        {
            return userCardExpenseRepository.GetById(id);
        }

        public bool Delete(UserCardExpense model)
        {
            model.LastModified = DateTime.Now;
            return userCardExpenseRepository.Update(model);
        }

        public bool Add(UserCardExpense model)
        {
            return userCardExpenseRepository.Add(model);
        }

        public bool Update(UserCardExpense model)
        {
            throw new NotImplementedException();
        }
    }
}
