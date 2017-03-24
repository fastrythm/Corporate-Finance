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
    public class BankPositionManagement : IBankPositionManagement
    {
        private readonly IBankPositionRepository bankPositionRepository;
        private readonly IUnitOfWork unitOfWork;


        public BankPositionManagement(IBankPositionRepository bankPositionRepository, IUnitOfWork unitOfWork)
        {
            this.bankPositionRepository = bankPositionRepository;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<CompanyBankPositionVM> GetAllBankPositionByParam(DateTime fromDate)
        {
            return bankPositionRepository.GetAllBankPositionByParam(fromDate);
        }
 
        public CompanyBankPosition GetBankPosition(long id)
        {
            return bankPositionRepository.GetById(id);
        }
        public bool Delete (CompanyBankPosition pettyCash)
        {
            return bankPositionRepository.Update(pettyCash);
           
        }
        public void SaveBankPosition()
        {
            unitOfWork.Commit();
        }

        public bool Add(CompanyBankPosition model)
        {
           return bankPositionRepository.Add(model);
        }

        public bool Update(CompanyBankPosition model)
        {
            return bankPositionRepository.Update(model);
        }
    }
}
