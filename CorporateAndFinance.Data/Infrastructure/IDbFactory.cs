using CorporateAndFinance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
       CorporateFinanceEntities Init();
    }
}
