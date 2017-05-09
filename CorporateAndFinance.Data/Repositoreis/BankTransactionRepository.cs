
using System;
using System.Collections.Generic;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using System.Linq;
using CorporateAndFinance.Core.Helper.Extension;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using log4net;

namespace CorporateAndFinance.Data.Repositories
{
    public class BankTransactionRepository : RepositoryBase<CompanyBankTransaction>, IBankTransactionRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(BankTransactionRepository));
        public BankTransactionRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<CompanyBankTransactionVM> GetAllBankTransactionByParam(CompanyBankTransactionVM param, DateTime fromDate, DateTime toDate)
        {
            try
            {

                IQueryable<CompanyBankTransactionVM> query = (from CBT in DbContext.CompanyBankTransactions.AsNoTracking()
                                                              join CB in DbContext.CompanyBanks.AsNoTracking() on CBT.CompanyBankID equals CB.CompanyBankID
                                                              join Comp in DbContext.Companies.AsNoTracking() on CB.CompanyID equals Comp.CompanyID
                                                              join Bnk in DbContext.Banks.AsNoTracking() on CB.BankID equals Bnk.BankID
                                                              where !CBT.IsDeleted
                                                              orderby CBT.TransactionDate ascending
                                                              where (DbFunctions.TruncateTime(CBT.TransactionDate) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(CBT.TransactionDate) <= DbFunctions.TruncateTime(toDate))
                                                              select new CompanyBankTransactionVM
                                                              {

                                                                  CompanyBankTransactionID = CBT.CompanyBankTransactionID,

                                                                  Description = CBT.Description,
                                                                  Amount = CBT.Amount,
                                                                  TransactionType = CBT.TransactionType,
                                                                  TransactionDate = CBT.TransactionDate,
                                                                  TransactionStatus = CBT.TransactionStatus,
                                                                  PaymentType = CBT.PaymentType,
                                                                  CategoryType = CBT.CategoryType,
                                                                  ReceiptNumber = CBT.ReceiptNumber,
                                                                  AccountNumber = CB.AccountNumber,
                                                                  CompanyName = Comp.Name,
                                                                  BankName = Bnk.Name,
                                                              });


                query = GetBankTransactionFiltersOrderQuery(query, param);


                int totalRecord = query.Count();

                var transaction = query.Skip(param.DTObject.start).Take(param.DTObject.length).ToList().Select(index => new CompanyBankTransactionVM
                {
                    CompanyBankTransactionID = index.CompanyBankTransactionID,
                    CompanyName = index.CompanyName,
                    BankName = index.BankName,
                    Description = index.Description,
                    Amount = index.Amount,
                    TransactionType = index.TransactionType,
                    TransactionDate = index.TransactionDate,
                    PaymentType = index.PaymentType,
                    CategoryType = index.CategoryType,
                    ReceiptNumber = index.ReceiptNumber,
                    AccountNumber = index.AccountNumber,
                    TransactionStatus = index.TransactionStatus,
                    DTObject = new DataTablesViewModel() { TotalRecordsCount = totalRecord }
                }).ToList();



                return transaction;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        private IQueryable<CompanyBankTransactionVM> GetBankTransactionFiltersOrderQuery(IQueryable<CompanyBankTransactionVM> query, CompanyBankTransactionVM param, bool forAll = false)
        {
            try
            {
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


                    if (columnData.search.value == null || string.IsNullOrEmpty(columnData.search.value.Trim()))
                        continue;
                    switch (columnData.data)
                    {
                        case "Description":
                            if (!forAll)
                                query = query.Where(col => col.Description.ToUpper().Contains(columnData.search.value.ToUpper()));
                            break;
                    }
                }

                if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                    query = query.Where(col => (
                        col.Description.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.TransactionType.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.PaymentType.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.CategoryType.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.ReceiptNumber.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.AccountNumber.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||

                        col.Amount.ToString().Contains(param.DTObject.search.value.ToUpper())));
                return query;


                return query;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public override bool Add(CompanyBankTransaction entity)
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

        public override bool Update(CompanyBankTransaction entity)
        {
            try
            {
                entity.LastModified = DateTime.Now;
                return base.Update(entity);
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return false;
            }
        }

        public List<BankTransactionReport> GetCollectionAndPaymentDetails(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var FromDate = new SqlParameter("@FromDate", fromDate.ToShortDateString());
                FromDate.Size = 25;
                var ToDate = new SqlParameter("@ToDate", toDate.ToShortDateString());
                ToDate.Size = 25;
                var result = DbContext.Database.SqlQuery<BankTransactionReport>("GetBankTransaction @FromDate,@ToDate", FromDate, ToDate).ToList();

                return result;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public List<InterCompanyReconciliationReport> InterCompanyReconciliation(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var FromDate = new SqlParameter("@FromDate", fromDate.ToShortDateString());
                FromDate.Size = 25;
                var ToDate = new SqlParameter("@ToDate", toDate.ToShortDateString());
                ToDate.Size = 25;
                var result = DbContext.Database.SqlQuery<InterCompanyReconciliationReport>("GetCompanyReconciliation @FromDate,@ToDate", FromDate, ToDate).ToList();

                return result;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public List<BankTransactionPaymentWiseReport> PaymentTypeWiseBankTransaction(DateTime fromDate, DateTime toDate, string paymentType)
        {
            try
            {
                var FromDate = new SqlParameter("@FromDate", fromDate.ToShortDateString());
                FromDate.Size = 25;
                var ToDate = new SqlParameter("@ToDate", toDate.ToShortDateString());
                ToDate.Size = 25;

                var paymentTypes = new SqlParameter("@PaymentTypes", paymentType);
                ToDate.Size = -1;

                var result = DbContext.Database.SqlQuery<BankTransactionPaymentWiseReport>("GetBankTransactionByPaymentTypes @FromDate,@ToDate,@PaymentTypes", FromDate, ToDate, paymentTypes).ToList();

                return result;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public List<BankReconciliationQBWiseReport> BankReconciliationQBWise(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var FromDate = new SqlParameter("@FromDate", fromDate.ToShortDateString());
                FromDate.Size = 25;
                var ToDate = new SqlParameter("@ToDate", toDate.ToShortDateString());
                ToDate.Size = 25;
                var result = DbContext.Database.SqlQuery<BankReconciliationQBWiseReport>("GetBankReconciliation @FromDate,@ToDate", FromDate, ToDate).ToList();

                return result;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }
    }

    public interface IBankTransactionRepository : IRepository<CompanyBankTransaction>
    {
        List<BankReconciliationQBWiseReport> BankReconciliationQBWise(DateTime fromDate, DateTime toDate);
        IEnumerable<CompanyBankTransactionVM> GetAllBankTransactionByParam(CompanyBankTransactionVM transaction, DateTime frdate, DateTime tdate);
        List<BankTransactionReport> GetCollectionAndPaymentDetails(DateTime fromDate, DateTime toDate);
        List<InterCompanyReconciliationReport> InterCompanyReconciliation(DateTime fromDate, DateTime toDate);
        List<BankTransactionPaymentWiseReport> PaymentTypeWiseBankTransaction(DateTime fromDate, DateTime toDate, string paymentType);
    }



}
