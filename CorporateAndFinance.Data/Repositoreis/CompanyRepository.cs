using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class CompanyRepository: RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<Company> GetAllCompanies()
        {
            throw new NotImplementedException();
        }
    }
    public interface ICompanyRepository : IRepository<Company>
    {
        IEnumerable<Company> GetAllCompanies();
    }

}
