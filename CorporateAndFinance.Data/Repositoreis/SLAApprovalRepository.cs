using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class SLAApprovalRepository : RepositoryBase<SLAApproval>, ISLAApprovalRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(RequisitionRepository));
        public SLAApprovalRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
        public interface ISLAApprovalRepository : IRepository<SLAApproval>
    {
       
    }
}
