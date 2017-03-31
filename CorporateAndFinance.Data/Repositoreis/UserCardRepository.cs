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
    public class UserCardRepository : RepositoryBase<UserCard>, IUserCardRepository
    {
        public UserCardRepository(IDbFactory dbFactory) : base(dbFactory){}
    }

    public interface IUserCardRepository : IRepository<UserCard>
    {

    }

    
    
}
