﻿using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface ICompanyManagement
    {
        IEnumerable<Company> GetAllCompanies();
        Company GetCompanyByNumber(long number);
        bool Add(Company model);
        void SaveCompany();
    }
}
