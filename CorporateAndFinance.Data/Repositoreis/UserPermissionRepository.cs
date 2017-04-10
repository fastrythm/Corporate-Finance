using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class UserPermissionRepository : RepositoryBase<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
       
    }
}
