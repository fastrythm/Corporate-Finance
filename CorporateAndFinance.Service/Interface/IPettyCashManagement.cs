using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IPettyCashManagement
    {
        IEnumerable<PettyCash> GetAllPettyCashByParam(PettyCash param, DateTime fromDate,DateTime toDate);
        void SavePettyCash();
        PettyCash GetPettyCash(long id);
        bool Delete(PettyCash pettyCash);
        bool Add(PettyCash model);
        PettyCashOpenCloseBalance GetOpeningClosingBalance(DateTime fromDate, DateTime toDate);
    }
}
