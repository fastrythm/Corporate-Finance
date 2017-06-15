using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using log4net;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(DepartmentRepository));
        public DepartmentRepository(IDbFactory dbFactory) : base(dbFactory) { }
       
        public override bool Add(Department entity)
        {
            try
            {
                entity.IsActive = true;
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
    public interface IDepartmentRepository : IRepository<Department>
    { 
    }

}
