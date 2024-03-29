﻿using CorporateAndFinance.Core.Model;
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
    public class UserExpenseManagement : IUserExpenseManagement
    {
        private readonly IUserExpenseRepository userExpenseRepository;
        private readonly IUnitOfWork unitOfWork;


        public UserExpenseManagement(IUserExpenseRepository userExpenseRepository, IUnitOfWork unitOfWork)
        {
            this.userExpenseRepository = userExpenseRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UserExpenseVM> GetAllUserExpensesByParam(UserExpenseVM param, DateTime fromDate, DateTime toDate)
        {
            return userExpenseRepository.GetAllUserExpensesByParam(param, fromDate, toDate);
        }

        public void SaveUserExpense()
        {
            unitOfWork.Commit();
        }


        public UserExpense GetUserExpense(string id)
        {
            var expenseId = new Guid(id);
            return userExpenseRepository.Get(x=>x.UserExpenseID == expenseId);
        }

        public bool Delete(UserExpense model)
        {
            model.LastModified = DateTime.Now;
            return userExpenseRepository.Update(model);
        }

        public bool Add(UserExpense model)
        {
            return userExpenseRepository.Add(model);
        }

        public bool Update(UserExpense model)
        {
            return userExpenseRepository.Update(model);
        }

        public IEnumerable<UserExpense> GetAllExpenseByDate(DateTime expenseDate)
        {
            return userExpenseRepository.GetMany(x => x.ExpenseDate.Month == expenseDate.Month && x.ExpenseDate.Year == expenseDate.Year);
        }

        public void DeleteAllByDate(DateTime expenseDate)
        {
            userExpenseRepository.Delete(x => x.ExpenseDate.Month == expenseDate.Month && x.ExpenseDate.Year == expenseDate.Year);
        }
    }
}
