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

      

        public override bool Add(Company entity)
        {
            entity.IsActive = true;
            entity.CreatedOn = DateTime.Now;
            entity.LastModified = DateTime.Now;
            return base.Add(entity);

        }
    }
    public interface ICompanyRepository : IRepository<Company>
    {
         
    }

}
