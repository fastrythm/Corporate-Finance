﻿using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using CorporateAndFinance.Core.Helper.Extension;
using System.Data.Entity;
using CorporateAndFinance.Core.Helper.Structure;
using log4net;

namespace CorporateAndFinance.Data.Repositories
{
    public class UserCardExpenseRepository : RepositoryBase<UserCardExpense>, IUserCardExpenseRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(UserCardExpenseRepository));
        public UserCardExpenseRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<UserCardExpenseVM> GetAllUserCardExpensesByParam(UserCardExpenseVM param, DateTime fromDate, DateTime toDate)
        {
            try
            {
                IQueryable<UserCardExpenseVM> query = (from uce in DbContext.UserCardExpenses.AsNoTracking()
                                                       join uc in DbContext.UserCards.AsNoTracking() on uce.UserCardID equals uc.UserCardID
                                                       join user in DbContext.Users.AsNoTracking() on uc.UserID.ToString() equals user.Id
                                                       join tempclient in DbContext.Companies.AsNoTracking()
                                                       on uce.ClientID equals tempclient.CompanyID into client
                                                       from tempclient in client.DefaultIfEmpty()
                                                       join tempconsult in DbContext.Consultants.AsNoTracking()
                                                       on uce.ConsultantID equals tempconsult.ConsultantID into consultant
                                                       from tempconsult in consultant.DefaultIfEmpty()
                                                       where !uce.IsDeleted
                                                       orderby uce.Date descending
                                                       where (DbFunctions.TruncateTime(uce.Date) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(uce.Date) <= DbFunctions.TruncateTime(toDate))
                                                       select new UserCardExpenseVM
                                                       {
                                                           UserCardExpenseID = uce.UserCardExpenseID,
                                                           RecieptNumber = uce.RecieptNumber,
                                                           UserCardNumber = uc.CardNumber,
                                                           Date = uce.Date,
                                                           Description = uce.Description,
                                                           CardHolderName = user.FirstName + " " + user.LastName,
                                                           CompanyName = uce.PaymentDetail,
                                                           ExpenseHead = uce.ExpenseHead,
                                                           ClientName = tempclient.Name,
                                                           ConsultantName = tempconsult.FirstName + " " + tempconsult.LastName,
                                                           BillType = uce.BillType,
                                                           Amount = uce.Amount,
                                                           IsSalesRelated = uce.IsSalesRelated,
                                                           Remarks = uce.Remarks,
                                                           TransactionType = uce.TransactionType
                                                       });


                query = GetUserCardExpenseFiltersOrderQuery(query, param);


                int totalRecord = query.Count();

                var userCardExpenseVM = query.Skip(param.DTObject.start).Take(param.DTObject.length).ToList().Select(index => new UserCardExpenseVM
                {
                    UserCardExpenseID = index.UserCardExpenseID,
                    RecieptNumber = index.RecieptNumber,
                    UserCardNumber = index.UserCardNumber,
                    Date = index.Date,
                    Description = index.Description,
                    CardHolderName = index.CardHolderName,
                    CompanyName = index.CompanyName,
                    ExpenseHead = index.ExpenseHead,
                    ClientName = index.ClientName,
                    ConsultantName = index.ConsultantName,
                    BillType = index.BillType,
                    Amount = index.Amount,
                    IsSalesRelated = index.IsSalesRelated,
                    Remarks = index.Remarks,
                    TransactionType = index.TransactionType,
                    DTObject = new DataTablesViewModel() { TotalRecordsCount = totalRecord }
                }).ToList();



                return userCardExpenseVM;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        private IQueryable<UserCardExpenseVM> GetUserCardExpenseFiltersOrderQuery(IQueryable<UserCardExpenseVM> query, UserCardExpenseVM param)
        {
            try
            {
                //   var expression = PredicateBuilder.True<AssetsEntity>();
                if (param == null)
                    return query;

                int index = -1;
                foreach (var columnData in param.DTObject.columns)
                {
                    index += 1;
                    if (columnData.orderable)
                    {
                        foreach (var row in param.DTObject.order.Where(i => i.column == index))
                        {
                            if (row.dir == "asc")
                                query = query.OrderBy(columnData.data);
                            else
                                query = query.OrderByDescending(columnData.data);
                        }
                    }


                }

                string id = Utility.CovertID(param.DTObject.search.value, "AX-");

                if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                    query = query.Where(col => (
                        col.Description.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.RecieptNumber.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.Remarks.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.UserCardNumber.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.CardHolderName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.Amount.ToString().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.CompanyName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.UserCardExpenseID.ToString().Equals(id)));
                return query;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }

        }

        public override bool Add(UserCardExpense entity)
        {
            try
            {

                entity.IsDeleted = false;
                entity.CreatedOn = DateTime.Now;
                entity.LastModified = DateTime.Now;
                return base.Add(entity);
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return false;
            }
        }

    }

    public interface IUserCardExpenseRepository : IRepository<UserCardExpense>
    {
        IEnumerable<UserCardExpenseVM> GetAllUserCardExpensesByParam(UserCardExpenseVM param, DateTime fromDate, DateTime toDate);

    }



}
