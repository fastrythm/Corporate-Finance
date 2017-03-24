using CorporateAndFinance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        CorporateFinanceEntities dbContext;

        public CorporateFinanceEntities Init()
        {
            return dbContext ?? (dbContext = new CorporateFinanceEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
