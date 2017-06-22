using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;

namespace CorporateAndFinance.Service.Implementation
{
    public class RequisitionApprovalManagement : IRequisitionApprovalManagement
    {
        private readonly IRequisitionApprovalRepository requisitionApprovalRepository;
        private readonly IUnitOfWork unitOfWork;

        public RequisitionApprovalManagement(IRequisitionApprovalRepository requisitionApprovalRepository, IUnitOfWork unitOfWork)
        {
            this.requisitionApprovalRepository = requisitionApprovalRepository;
            this.unitOfWork = unitOfWork;
        }
        public bool Add(RequisitionApproval model)
        {
            return requisitionApprovalRepository.Add(model);
        }

        public bool Delete(RequisitionApproval model)
        {
            return requisitionApprovalRepository.Delete(model);
        }

        public IEnumerable<RequisitionApproval> GetAllRequisitionApprovalByRequisition(long id)
        {
            return requisitionApprovalRepository.GetMany(x => x.RequisitionID == id);
        }

        public RequisitionApproval GetRequisitionApproval(long id)
        {
           return requisitionApprovalRepository.GetById(id);
        }

        public void SaveRequisitionApproval()
        {
            unitOfWork.Commit();
        }

        public bool Update(RequisitionApproval model)
        {
            return requisitionApprovalRepository.Update(model);
        }
    }
}
