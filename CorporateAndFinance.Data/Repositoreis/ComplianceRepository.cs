using System;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Core.Helper.Extension;
using CorporateAndFinance.Core.Helper.Structure;

namespace CorporateAndFinance.Data.Repositories
{
    public class ComplianceRepository : RepositoryBase<CompanyCompliance>, IComplianceRepository
    {
        public ComplianceRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<CompanyComplianceVM> GetAllCompliancesByParam(CompanyComplianceVM param, DateTime fromDate, DateTime toDate)
        {
            IQueryable<CompanyComplianceVM> query = (from record in DbContext.CompanyCompliances.AsNoTracking()
                                                   join comp in DbContext.Companies.AsNoTracking() on record.CompanyID equals comp.CompanyID
                                                   orderby record.Date descending
                                                   where (DbFunctions.TruncateTime(record.Date) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(record.Date) <= DbFunctions.TruncateTime(toDate))
                                                   select new CompanyComplianceVM {

                                                       CompanyComplianceID = record.CompanyComplianceID,
                                                       LegalAuthorityName = record.LegalAuthorityName,
                                                       Description = record.Description,
                                                       Remarks1 = record.Remarks1,
                                                       Remarks2 = record.Remarks2,
                                                       GeneralRemarks = record.GeneralRemarks,
                                                       Date = record.Date,
                                                       CompanyName = comp.Name,
                                                       Amount = record.Amount,
                                                       Status = record.Status
                                                   });


            query = GetComplianceFiltersOrderQuery(query, param, true);


            int totalRecord = query.Count();

            var complianceList = query.Skip(param.DTObject.start).Take(param.DTObject.length).ToList().Select(index => new CompanyComplianceVM
            {
                CompanyComplianceID = index.CompanyComplianceID,
                Description = index.Description,
                Remarks1 = index.Remarks1,
                Remarks2 = index.Remarks2,
                GeneralRemarks = index.GeneralRemarks,
                LegalAuthorityName = index.LegalAuthorityName,
                Date = index.Date,
                CompanyName = index.CompanyName,
                Amount = index.Amount,
                Status = index.Status,
                DTObject = new DataTablesViewModel() { TotalRecordsCount = totalRecord }
            }).ToList();



            return complianceList;
            
        }


        private IQueryable<CompanyComplianceVM> GetComplianceFiltersOrderQuery(IQueryable<CompanyComplianceVM> query, CompanyComplianceVM param, bool forAll = false)
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

            string id = Utility.CovertID(param.DTObject.search.value, "CC-");

            if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                query = query.Where(col => (
                    col.Description.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                    col.LegalAuthorityName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                    col.Status.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                    col.Remarks1.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                    col.Remarks2.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                    col.GeneralRemarks.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                    col.CompanyComplianceID.ToString().Equals(id)));


            return query;


            return query;
        }
    }
    public interface IComplianceRepository : IRepository<CompanyCompliance>
    {
     IEnumerable<CompanyComplianceVM> GetAllCompliancesByParam(CompanyComplianceVM param, DateTime fromDate, DateTime toDate);
    }
}