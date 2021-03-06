﻿using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IUserExpenseManagement
    {
        IEnumerable<UserExpenseVM> GetAllUserExpensesByParam(UserExpenseVM param, DateTime fromDate,DateTime toDate);
        UserExpense GetUserExpense(string id);
        void SaveUserExpense();
        bool Delete(UserExpense model);
        bool Add(UserExpense model);
        bool Update(UserExpense model);
        IEnumerable<UserExpense> GetAllExpenseByDate(DateTime expenseDate);
        void DeleteAllByDate(DateTime expenseDate);
    }
}
