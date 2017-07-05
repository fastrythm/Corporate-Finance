using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using CorporateAndFinance.Core.Helper.Extension;
using System.Data.Entity;
 
namespace CorporateAndFinance.Data.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory){}

        public IEnumerable<ApplicationUser> GetAllUsersByDepartments(IEnumerable<UserDepartment> departments)
        {
           
            List<long> deptId = new List<long>();
            foreach (var depts in departments)
                deptId.Add(depts.DepartmentID);

            IEnumerable<ApplicationUser> query = (from user in DbContext.Users.AsNoTracking()
                         join dept in DbContext.UserDepartments.AsNoTracking() on user.Id equals dept.UserID
                        where dept.IsActive && !user.IsDeleted
                        orderby user.FirstName ascending
                     where  (deptId.Contains(dept.DepartmentID))
                     select user).Distinct().ToList();

            return query;
        }

        public IEnumerable<ApplicationUser> GetAllUsersByRoleAndDepartment(string roleId, long departmentId)
        {
            var query = (from user in DbContext.Users.AsNoTracking()
                         join userdept in DbContext.UserDepartments.AsNoTracking() on user.Id equals userdept.UserID
                         where userdept.IsActive && !user.IsDeleted
                         orderby user.FirstName ascending
                         where (userdept.RoleID == roleId) && userdept.DepartmentID == departmentId
                         select user).Distinct().ToList();

            return query;
        }
    }

    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetAllUsersByDepartments(IEnumerable<UserDepartment> departments);
        IEnumerable<ApplicationUser> GetAllUsersByRoleAndDepartment(string roleId, long departmentId);
    }



}
