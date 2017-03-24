using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositories;
using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Implementation
{
    public class PettyCashManagement : IPettyCashManagement
    {
        private readonly IPettyCashRepository pettyCashRepository;
        private readonly IUnitOfWork unitOfWork;


        public PettyCashManagement(IPettyCashRepository pettyCashRepository, IUnitOfWork unitOfWork)
        {
            this.pettyCashRepository = pettyCashRepository;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<PettyCash> GetAllPettyCashByParam(PettyCash param, DateTime fromDate,DateTime toDate)
        {
            return pettyCashRepository.GetAllPettyCashByParam(param, fromDate,toDate);
        }

       public PettyCashOpenCloseBalance GetOpeningClosingBalance(DateTime fromDate, DateTime toDate)
        {
            return pettyCashRepository.GetOpeningClosingBalance(fromDate,toDate);
        }

        public PettyCash GetPettyCash(long id)
        {
            return pettyCashRepository.GetById(id);
        }
        public bool Delete (PettyCash pettyCash)
        {
            return  pettyCashRepository.Update(pettyCash);
           
        }
        public void SavePettyCash()
        {
            unitOfWork.Commit();
        }

        public bool Add(PettyCash model)
        {
           return pettyCashRepository.Add(model);
        }
    }
}
