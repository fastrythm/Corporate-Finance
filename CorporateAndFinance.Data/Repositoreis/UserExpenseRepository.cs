using CorporateAndFinance.Core.Model;
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
    public class UserExpenseRepository : RepositoryBase<UserExpense>, IUserExpenseRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(UserCardExpenseRepository));
        public UserExpenseRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<UserExpenseVM> GetAllUserExpensesByParam(UserExpenseVM param, DateTime fromDate, DateTime toDate)
        {
            
            try
            {
                IQueryable<UserExpenseVM> query = (    from ue in DbContext.UserExpenses.AsNoTracking()
                                                       join user in DbContext.Users.AsNoTracking() on ue.UserID.ToString() equals user.Id
                                                       join dept in DbContext.Departments.AsNoTracking() on ue.DepartmentID equals dept.DepartmentID                                                     
                                                       orderby ue.ExpenseDate descending
                                                       where (DbFunctions.TruncateTime(ue.ExpenseDate) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(ue.ExpenseDate) <= DbFunctions.TruncateTime(toDate) && ue.IsActive)
                                                       select new UserExpenseVM
                                                       {
                                                           UserExpenseID = ue.UserExpenseID,
                                                           SerialNumber = ue.SerialNumber,
                                                           UserName = user.FirstName +" "+user.LastName,
                                                           UserNumber = user.EmployeeNumber,
                                                           DepartmentName = dept.Name,
                                                           ExpenseDate = ue.ExpenseDate,
                                                           Monthly_Salary = ue.Monthly_Salary,
                                                           Monthly_Salary2 = ue.Monthly_Salary2,
                                                           EOBI_Employer = ue.EOBI_Employer,
                                                           PF_Employer = ue.PF_Employer,
                                                           Mobile_Allowance = ue.Mobile_Allowance,
                                                           Bonus = ue.Bonus,
                                                           Meal_Reimbursement = ue.Meal_Reimbursement,
                                                           Transportation = ue.Transportation,
                                                           Leave_Encashment = ue.Leave_Encashment,
                                                           Incentive_PSM = ue.Incentive_PSM,
                                                           Health_Insurance = ue.Health_Insurance,
                                                           Medical_OPD = ue.Medical_OPD,
                                                           Billable_Salary_PKR = ue.Billable_Salary_PKR,
                                                           Billable_Salary_USD = ue.Billable_Salary_USD
                                                       });

                query = GetUserExpenseFiltersOrderQuery(query, param);


                int totalRecord = query.Count();

                var userExpenseVM = query.Skip(param.DTObject.start).Take(param.DTObject.length).ToList().Select(index => new UserExpenseVM
                {
                    UserExpenseID = index.UserExpenseID,
                    SerialNumber = index.SerialNumber,
                    UserName = index.UserName,
                    UserNumber = index.UserNumber,
                    DepartmentName = index.DepartmentName,
                    ExpenseDate = index.ExpenseDate,
                    Monthly_Salary = index.Monthly_Salary,
                    Monthly_Salary2 = index.Monthly_Salary2,
                    EOBI_Employer = index.EOBI_Employer,
                    PF_Employer = index.PF_Employer,
                    Mobile_Allowance = index.Mobile_Allowance,
                    Bonus = index.Bonus,
                    Meal_Reimbursement = index.Meal_Reimbursement,
                    Transportation = index.Transportation,
                    Leave_Encashment = index.Leave_Encashment,
                    Incentive_PSM = index.Incentive_PSM,
                    Health_Insurance = index.Health_Insurance,
                    Medical_OPD = index.Medical_OPD,
                    Billable_Salary_PKR = index.Billable_Salary_PKR,
                    Billable_Salary_USD = index.Billable_Salary_USD,
                    DTObject = new DataTablesViewModel() { TotalRecordsCount = totalRecord }
                }).ToList();



                return userExpenseVM;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        private IQueryable<UserExpenseVM> GetUserExpenseFiltersOrderQuery(IQueryable<UserExpenseVM> query, UserExpenseVM param)
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

               // string id = Utility.CovertID(param.DTObject.search.value, "UE-");

                if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                    query = query.Where(col => (
                        col.DepartmentName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.UserName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.UserNumber.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.Billable_Salary_PKR.ToString().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.Billable_Salary_USD.ToString().Contains(param.DTObject.search.value.ToUpper())
                         ));
                return query;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }

        }

        public override bool Add(UserExpense entity)
        {
            try
            {
                entity.IsActive = true;
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

    public interface IUserExpenseRepository : IRepository<UserExpense>
    {
        IEnumerable<UserExpenseVM> GetAllUserExpensesByParam(UserExpenseVM param, DateTime fromDate, DateTime toDate);

    }



}
