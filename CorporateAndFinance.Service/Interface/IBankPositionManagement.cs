using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IBankPositionManagement
    {
        IEnumerable<CompanyBankPositionVM> GetAllBankPositionByParam(DateTime fromDate);
        void SaveBankPosition();
        CompanyBankPosition GetBankPosition(long id);
        bool Delete(CompanyBankPosition bankPosition);
        bool Add(CompanyBankPosition model);
        bool Update(CompanyBankPosition model);
    }
}
