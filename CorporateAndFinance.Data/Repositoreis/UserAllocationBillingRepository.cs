using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using log4net;

namespace CorporateAndFinance.Data.Repositories
{
    public class UserAllocationBillingRepository : RepositoryBase<UserAllocationBilling>, IUserAllocationBillingRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(UserAllocationBillingRepository));
        public UserAllocationBillingRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<UserAllocationBilling> GetAllUserAllocationBillingByParam(UserAllocationBilling param, DateTime fromDate, DateTime toDate)
        {
            
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        //private IQueryable<UserAllocationBilling> GetUserAllocationBillingFiltersOrderQuery(IQueryable<UserAllocationBilling> query, UserAllocationBilling param)
        //{
        //    try
        //    {
        //        //   var expression = PredicateBuilder.True<AssetsEntity>();
        //        if (param == null)
        //            return query;

        //        int index = -1;
        //        foreach (var columnData in param.DTObject.columns)
        //        {
        //            index += 1;
        //            if (columnData.orderable)
        //            {
        //                foreach (var row in param.DTObject.order.Where(i => i.column == index))
        //                {
        //                    if (row.dir == "asc")
        //                        query = query.OrderBy(columnData.data);
        //                    else
        //                        query = query.OrderByDescending(columnData.data);
        //                }
        //            }


        //        }

        //        string id = Utility.CovertID(param.DTObject.search.value, "UE-");

        //        if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
        //            query = query.Where(col => (
        //                col.DepartmentName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
        //                col.UserName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
        //                col.UserNumber.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
        //                col.Billable_Salary_PKR.ToString().Contains(param.DTObject.search.value.ToUpper()) ||
        //                 col.Billable_Salary_USD.ToString().Contains(param.DTObject.search.value.ToUpper()) ||
        //                col.UserExpenseID.ToString().Equals(id)));
        //        return query;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
        //        return null;
        //    }

        //}

        public override bool Add(UserAllocationBilling entity)
        {
            try
            {
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

    public interface IUserAllocationBillingRepository : IRepository<UserAllocationBilling>
    {
        IEnumerable<UserAllocationBilling> GetAllUserAllocationBillingByParam(UserAllocationBilling param, DateTime fromDate, DateTime toDate);

    }



}
