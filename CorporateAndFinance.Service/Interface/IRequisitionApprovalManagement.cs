﻿using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface IRequisitionApprovalManagement
    {
        IEnumerable<RequisitionApproval> GetAllRequisitionApprovalByParam(DateTime fromDate,DateTime toDate);
        void SaveRequisitionApproval();
        RequisitionApproval GetRequisitionApproval(long id);
        bool Delete(RequisitionApproval model);
        bool Add(RequisitionApproval model);
        bool Update(RequisitionApproval model);
    }
}