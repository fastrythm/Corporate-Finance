using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class CompanyRepository: RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbFactory dbFactory) : base(dbFactory) { }

      

        public override bool Add(Company entity)
        {
            entity.IsActive = true;
            entity.CreatedOn = DateTime.Now;
            entity.LastModified = DateTime.Now;
            return base.Add(entity);

        }

        public IEnumerable<CompanyBankVM> GetCompanyBankAccounts()
        {
            IList<CompanyBankVM> query = (
                                                  from cb in DbContext.CompanyBanks.AsNoTracking()
                                                  join comp in DbContext.Companies.AsNoTracking() on cb.CompanyID equals comp.CompanyID
                                                  join bk in DbContext.Banks.AsNoTracking() on cb.BankID equals bk.BankID
                                                  orderby comp.Name ascending
                                                  where cb.IsActive
                                                  select new CompanyBankVM
                                                  {
                                                      CompanyID = cb.CompanyID,
                                                      BankID = cb.BankID,
                                                      CompanyName = comp.Name,
                                                      BankName = bk.Name,
                                                      AccountNumber = cb.AccountNumber,
                                                      CompanyBankID = cb.CompanyBankID
                                                  }).ToList();


            return query;
        }
    }
    public interface ICompanyRepository : IRepository<Company>
    {
        IEnumerable<CompanyBankVM> GetCompanyBankAccounts();
    }

}
