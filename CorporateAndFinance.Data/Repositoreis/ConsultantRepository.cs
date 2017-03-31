using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class ConsultantRepository : RepositoryBase<Consultant>, IConsultantRepository
    {
        public ConsultantRepository(IDbFactory dbFactory) : base(dbFactory) { }

      

        public override bool Add(Consultant entity)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.CreatedOn = DateTime.Now;
            entity.LastModified = DateTime.Now;
            return base.Add(entity);

        }
    }
    public interface IConsultantRepository : IRepository<Consultant>
    {
         
    }

}
