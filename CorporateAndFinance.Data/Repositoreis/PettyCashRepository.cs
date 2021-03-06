﻿
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using CorporateAndFinance.Core.Helper.Extension;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using CorporateAndFinance.Core.Helper.Structure;
using log4net;

namespace CorporateAndFinance.Data.Repositories
{
    public class PettyCashRepository : RepositoryBase<PettyCash>, IPettyCashRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(PettyCashRepository));
        public PettyCashRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<PettyCash> GetAllPettyCashByParam(PettyCash param, DateTime fromDate, DateTime toDate)
        {
            try
            {
                IQueryable<PettyCash> query = (from Record in DbContext.PettyCashes.AsNoTracking()
                                               where !Record.IsDeleted
                                               orderby Record.TransactionDate ascending
                                               where (DbFunctions.TruncateTime(Record.TransactionDate) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(Record.TransactionDate) <= DbFunctions.TruncateTime(toDate))
                                               select Record);


                query = GetPettyCashFiltersOrderQuery(query, param);


                int totalRecord = query.Count();

                var pettyCashList = query.Skip(param.DTObject.start).Take(param.DTObject.length).ToList().Select(index => new PettyCash
                {
                    PettyCashID = index.PettyCashID,
                    Description = index.Description,
                    Amount = index.Amount,
                    ChartOfAccountTitle = index.ChartOfAccountTitle,
                    TransactionType = index.TransactionType,
                    TransactionDate = index.TransactionDate,

                    DTObject = new DataTablesViewModel() { TotalRecordsCount = totalRecord }
                }).ToList();



                return pettyCashList;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }


        private IQueryable<PettyCash> GetPettyCashFiltersOrderQuery(IQueryable<PettyCash> query, PettyCash param, bool forAll = false)
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

                string id = Utility.CovertID(param.DTObject.search.value, "PC-");

                if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                    query = query.Where(col => (
                        col.Description.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.TransactionType.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.Amount.ToString().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.PettyCashID.ToString().Equals(id)));
                return query;


                return query;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public override bool Add(PettyCash entity)
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

        public override bool Update(PettyCash entity)
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

        public PettyCashOpenCloseBalance GetOpeningClosingBalance(DateTime fromDate, DateTime toDate)
        {
            try
            {
                DateTime temporaryDate = toDate.AddDays(1);

                List<PettyCash> query = (from Record in DbContext.PettyCashes.AsNoTracking()
                                         where !Record.IsDeleted
                                         orderby Record.TransactionDate ascending
                                         where (DbFunctions.TruncateTime(Record.TransactionDate) <= DbFunctions.TruncateTime(temporaryDate))
                                         select Record).ToList();

                decimal debitedAmount  = query.Where(a => a.TransactionType.Equals("Credit") && a.TransactionDate.Date < fromDate.Date).Select(x => x.Amount).Sum();
                decimal creditedAmount = query.Where(a => a.TransactionType.Equals("Debit") && a.TransactionDate.Date < fromDate.Date).Select(x => x.Amount).Sum();

                PettyCashOpenCloseBalance openCloseBalance = new PettyCashOpenCloseBalance();
                openCloseBalance.OpeningBalance = creditedAmount - debitedAmount;


                creditedAmount  = query.Where(a => a.TransactionType.Equals("Debit") && a.TransactionDate.Date >= fromDate.Date && a.TransactionDate.Date <= toDate.Date).Select(x => x.Amount).Sum();
                debitedAmount = query.Where(a => a.TransactionType.Equals("Credit") && a.TransactionDate.Date >= fromDate.Date && a.TransactionDate.Date <= toDate.Date).Select(x => x.Amount).Sum();

                openCloseBalance.ClosingBalance = openCloseBalance.OpeningBalance - debitedAmount + creditedAmount;

                return openCloseBalance;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }
    }

    public interface IPettyCashRepository : IRepository<PettyCash>
    {
        IEnumerable<PettyCash> GetAllPettyCashByParam(PettyCash param, DateTime fromDate, DateTime toDate);
        PettyCashOpenCloseBalance GetOpeningClosingBalance(DateTime fromDate, DateTime toDate);
    }



}
