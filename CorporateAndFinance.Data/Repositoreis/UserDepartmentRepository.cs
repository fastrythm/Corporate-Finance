using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using log4net;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class UserDepartmentRepository : RepositoryBase<UserDepartment>, IUserDepartmentRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(DepartmentRepository));
        public UserDepartmentRepository(IDbFactory dbFactory) : base(dbFactory) { }
       
        public override bool Add(UserDepartment entity)
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

        public IEnumerable<UserDepartmentVM> GetAllUserDepartmentByUserId(string userId)
        {
            List<UserDepartmentVM> query = (from userDept in DbContext.UserDepartments.AsNoTracking()
                                             join dept in DbContext.Departments.AsNoTracking()
                                             on userDept.DepartmentID equals dept.DepartmentID
                                             where userDept.IsActive 
                                              
                                               select new UserDepartmentVM
                                               {
                                                   UserDepartmentID = userDept.UserDepartmentID,
                                                   UserID = userDept.UserID,
                                                   DepartmentName = dept.Name,
                                                   DepartmentID = dept.DepartmentID,
                                                   IsPrimary = userDept.IsPrimary,
                                                   IsActive = userDept.IsActive,
                                                   StartDate = userDept.StartDate,
                                                   EndDate = userDept.EndDate
                                               }).ToList();


            return query;
        }

        public override bool Update(UserDepartment entity)
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
    }
    public interface IUserDepartmentRepository : IRepository<UserDepartment>
    {
        IEnumerable<UserDepartmentVM> GetAllUserDepartmentByUserId(string userId);
    }

}
