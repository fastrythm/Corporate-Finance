using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IConsultantManagement
    {
        Consultant GetConsultantByNumber(int number);
        bool Add(Consultant model);
        void SaveConsultant();
    }
}
