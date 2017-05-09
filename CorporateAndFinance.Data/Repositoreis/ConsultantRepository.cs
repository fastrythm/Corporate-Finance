using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using log4net;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class ConsultantRepository : RepositoryBase<Consultant>, IConsultantRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(ConsultantRepository));
        public ConsultantRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public override bool Add(Consultant entity)
        {
            try
            {
                entity.IsActive = true;
                entity.IsDeleted = false;
                entity.CreatedOn = DateTime.Now;
                entity.LastModified = DateTime.Now;
                return base.Add(entity);
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
    public interface IConsultantRepository : IRepository<Consultant>
    {

    }

}
