using System;
using System.Collections.Generic;
using System.Data;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositories;
using CorporateAndFinance.Service.Interface;

namespace CorporateAndFinance.Service.Implementation
{
    public class BankTransactionManagement : IBankTransactionManagement
    {
        private readonly IBankTransactionRepository bankTransactionRepository;
        private readonly IUnitOfWork unitOfWork;

        public BankTransactionManagement(IBankTransactionRepository bankTransactionRepository, IUnitOfWork unitOfWork)
        {
            this.bankTransactionRepository = bankTransactionRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool Add(CompanyBankTransaction model)
        {
            return bankTransactionRepository.Add(model);
        }

        public bool Delete(CompanyBankTransaction transaction)
        {
            return bankTransactionRepository.Update(transaction);
        }

        public IEnumerable<CompanyBankTransactionVM> GetAllBankTransactionByParam(CompanyBankTransactionVM transaction, DateTime frdate, DateTime tdate)
        {
            return bankTransactionRepository.GetAllBankTransactionByParam(transaction, frdate, tdate);
        }

        public List<BankTransactionReport> GetCollectionAndPaymentDetails(DateTime fromDate, DateTime toDate)
        {
            return bankTransactionRepository.GetCollectionAndPaymentDetails(fromDate, toDate);
        }
        public  List<InterCompanyReconciliationReport> InterCompanyReconciliation(DateTime fromDate, DateTime toDate)
        {
            return bankTransactionRepository.InterCompanyReconciliation(fromDate, toDate);
        }

        public CompanyBankTransaction GetCompanyBankTransaction(long id)
        {
            return bankTransactionRepository.GetById(id);
        }

        public void SaveCompanyBankTransaction()
        {
            unitOfWork.Commit();
        }

        public bool Update(CompanyBankTransaction model)
        {
            return bankTransactionRepository.Update(model);
        }
    }
}
