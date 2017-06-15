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
    public class RequisitionApprovalRepository : RepositoryBase<RequisitionApproval>, IRequisitionApprovalRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(RequisitionApprovalRepository));
        public RequisitionApprovalRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<RequisitionApproval> GetAllRequisitionApprovalByParam(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public override bool Add(RequisitionApproval entity)
        {
            try
            {
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

        public override bool Update(RequisitionApproval entity)
        {
            try
            {
                entity.LastModified = DateTime.Now;
                return base.Update(entity);

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return false;
            }

        }
    }
    public interface IRequisitionApprovalRepository : IRepository<RequisitionApproval>
    {
        IEnumerable<RequisitionApproval> GetAllRequisitionApprovalByParam(DateTime fromDate,DateTime toDate);
    }
}
