﻿using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Core.Helper.Structure;

namespace CorporateAndFinance.Service.Implementation
{
    public class RequisitionManagement : IRequisitionManagement
    {
        private readonly IRequisitionRepository requisitionRepository;
        private readonly IUnitOfWork unitOfWork;

        public RequisitionManagement(IRequisitionRepository requisitionRepository, IUnitOfWork unitOfWork)
        {
            this.requisitionRepository = requisitionRepository;
            this.unitOfWork = unitOfWork;
        }
        public bool Add(Requisition model)
        {
            return requisitionRepository.Add(model);
        }

        public bool DeAttach(Requisition model)
        {
            return requisitionRepository.DeAttach(model);
        }

        public bool Delete(Requisition model)
        {
            return requisitionRepository.Delete(model);
        }

        public IEnumerable<Requisition> GetAllApprovedRequisition()
        {
            return requisitionRepository.GetMany(x => x.Status.Equals(RequisitionStatus.Level2_Approved));
        }

        public IEnumerable<RequisitionVM> GetAllRequisitionByParam(RequisitionVM param, DateTime fromDate, DateTime toDate,IEnumerable<UserDepartment> departments,bool isAdmin,string type)
        {
            return requisitionRepository.GetAllRequisitionByParam(param,fromDate, toDate, departments, isAdmin, type);
        }

       

        public Requisition GetRequisition(long id)
        {
           return requisitionRepository.GetById(id);
        }

        public RequisitionVM GetRequisitionCompleteInfoById(long requisitionId)
        {
            return requisitionRepository.GetRequisitionCompleteInfoById(requisitionId);
        }

        public void SaveRequisition()
        {
            unitOfWork.Commit();
        }

        public bool Update(Requisition model)
        {
            return requisitionRepository.Update(model);
        }
    }
}
