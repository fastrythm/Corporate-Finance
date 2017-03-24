
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using CorporateAndFinance.Core.Helper.Extension;
using System.Data.Entity;

namespace CorporateAndFinance.Data.Repositories
{
    public class BankPositionRepository : RepositoryBase<CompanyBankPosition>, IBankPositionRepository
    {
        public BankPositionRepository(IDbFactory dbFactory) : base(dbFactory) { }

       public  IEnumerable<CompanyBankPositionVM> GetAllBankPositionByParam(DateTime fromDate)
       {


            IList<CompanyBankPositionVM> query = (from cb in DbContext.CompanyBanks.AsNoTracking()
                                                  join comp in DbContext.Companies.AsNoTracking() on cb.CompanyID equals comp.CompanyID
                                                  join bk in DbContext.Banks.AsNoTracking() on cb.BankID equals bk.BankID
                                                  
                                                  join cbp in DbContext.CompanyBankPositions.AsNoTracking().Where(p=> DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(fromDate))
                                                  on cb.CompanyBankID equals cbp.CompanyBankID into temp
                                                  from tempcbp in temp.DefaultIfEmpty()    
                                                  orderby cb.CompanyID ascending
 
                                                  select new CompanyBankPositionVM
                                                  {
                                                      CompanyID = cb.CompanyID,
                                                      CompanyBankPositionID = tempcbp.CompanyBankPositionID,
                                                      CompanyName = comp.Name,
                                                      PositionDate = tempcbp.Date,
                                                      Balance = tempcbp.Amount,
                                                      AccountNumber = cb.AccountNumber,
                                                      AccountType = cb.AccountType,
                                                      BankName = bk.Name,
                                                      CompanyBankID = cb.CompanyBankID
                                                  }).ToList();

           
            return query;
        }


        private IQueryable<PettyCash> GetPettyCashFiltersOrderQuery(IQueryable<PettyCash> query, PettyCash param, bool forAll = false)
        {
            //   var expression = PredicateBuilder.True<AssetsEntity>();
            if (param == null)
                return query;

            int index = -1;
            foreach (var columnData in param.DTObject.columns)
            {
                index += 1;
                if (columnData.orderable)
                {
                    foreach (var row in param.DTObject.order.Where(i => i.column == index))
                    {
                        if (row.dir == "asc")
                            query = query.OrderBy(columnData.data);
                        else
                            query = query.OrderByDescending(columnData.data);
                    }
                }


                if (columnData.search.value == null || string.IsNullOrEmpty(columnData.search.value.Trim()))
                    continue;
                switch (columnData.data)
                {
                    case "Description":
                        if (!forAll)
                            query = query.Where(col => col.Description.ToUpper().Contains(columnData.search.value.ToUpper()));
                        break;
                }
            }

            if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                query = query.Where(col => (
                    col.Description.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                    col.TransactionType.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                    col.Amount.ToString().Contains(param.DTObject.search.value.ToUpper())));
            return query;


            return query;
        }

        public override bool Add(CompanyBankPosition entity)
        {
            
           // entity.cr = DateTime.Now;
         //   entity.LastModified = DateTime.Now;
            return base.Add(entity);

        }

        public override bool Update(CompanyBankPosition entity)
        {
           // entity.LastModified = DateTime.Now;
            return base.Update(entity);
            
        }
 
    }

    public interface IBankPositionRepository : IRepository<CompanyBankPosition>
    {
        IEnumerable<CompanyBankPositionVM> GetAllBankPositionByParam(DateTime fromDate);
  
    }

    
    
}
