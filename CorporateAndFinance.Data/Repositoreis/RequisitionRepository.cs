using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CorporateAndFinance.Core.Helper.Extension;
using CorporateAndFinance.Core.Helper.Structure;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class RequisitionRepository : RepositoryBase<Requisition>, IRequisitionRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(RequisitionRepository));
        public RequisitionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<RequisitionVM> GetAllRequisitionByParam(RequisitionVM param,DateTime fromDate, DateTime toDate, IEnumerable<UserDepartment> departments, bool isAdmin, string type)
        {
            try
            {
                List<long> deptId = new List<long>();
                foreach (var dept in departments)
                    deptId.Add(dept.DepartmentID);

                IQueryable<RequisitionVM> query = null;
                if (type == RequestStatus.My_Request)
                {
                      query = (from req in DbContext.Requisitions.AsNoTracking()
                                                       where !req.IsDeleted
                                                       orderby req.RequisitionDate descending
                                                       where (DbFunctions.TruncateTime(req.RequisitionDate) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(req.RequisitionDate) <= DbFunctions.TruncateTime(toDate))
                                                       && (deptId.Contains(req.DepartmentID) || isAdmin)
                                                       select new RequisitionVM
                                                       {
                                                           RequisitionID = req.RequisitionID,
                                                           JobTitle = req.JobTitle,
                                                           RequisitionDate = req.RequisitionDate,
                                                           Status = req.Status,
                                                           NoOfPosition = req.NoOfPosition,
                                                           GradeLevel = req.GradeLevel,
                                                           DepartmentID = req.DepartmentID,
                                                           RequisitionApprovalID = 0
                                                       });

                }
                else
                {

                   // int condition = CheckHaveRight(deptId);

                    query = (from req in DbContext.Requisitions.AsNoTracking()
                             join reqApp in DbContext.RequisitionApprovals.AsNoTracking()
                             on req.RequisitionID equals reqApp.RequisitionID
                             where !req.IsDeleted && !reqApp.IsActive
                             orderby req.RequisitionDate descending
                             where (DbFunctions.TruncateTime(req.RequisitionDate) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(req.RequisitionDate) <= DbFunctions.TruncateTime(toDate))
                             &&  ((type == RequisitionStatus.Level2_Pending && (req.Status.Equals(RequisitionStatus.Level1_Approved) || req.Status.Equals(RequisitionStatus.Level2_Pending)) )  || (req.Status.Equals(type)) )  
                             && deptId.Contains(reqApp.DepartmentID)
                             select new RequisitionVM
                             {
                                 RequisitionID = req.RequisitionID,
                                 JobTitle = req.JobTitle,
                                 RequisitionDate = req.RequisitionDate,
                                 Status = req.Status,
                                 NoOfPosition = req.NoOfPosition,
                                 GradeLevel = req.GradeLevel,
                                 DepartmentID = req.DepartmentID,
                                 RequisitionApprovalID = reqApp.RequisitionApprovalID
                             });
                }
                 query = GetRequisitionFiltersOrderQuery(query, param);

               
                

                int totalRecord = query.Count();

                var requisition = query.Skip(param.DTObject.start).Take(param.DTObject.length).ToList().Select(index => new RequisitionVM
                {
                    RequisitionID = index.RequisitionID,
                    JobTitle = index.JobTitle,
                    RequisitionDate = index.RequisitionDate,
                    Status = index.Status,
                    NoOfPosition = index.NoOfPosition,
                    GradeLevel = index.GradeLevel,
                    RequisitionApprovalID = index.RequisitionApprovalID,
                    DTObject = new DataTablesViewModel() { TotalRecordsCount = totalRecord }
                }).ToList();



                return requisition;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        private int CheckHaveRight(List<long> deptId)
        {
            var query = (from sla in DbContext.SLAApprovals.AsNoTracking()
                         where (deptId.Contains(sla.DepartmentID)) && sla.SLAType.Equals(SLAType.Requisition)
                         select sla).ToList() ;

            if (query != null && query.Count > 0)
                return 1;

            return 0;
        }

        private IQueryable<RequisitionVM> GetRequisitionFiltersOrderQuery(IQueryable<RequisitionVM> query, RequisitionVM param, bool forAll = false)
        {
            try
            {
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
                        case "JobTitle":
                            if (!forAll)
                                query = query.Where(col => col.JobTitle.ToUpper().Contains(columnData.search.value.ToUpper()));
                            break;
                    }
                }

                string id = Utility.CovertID(param.DTObject.search.value, "UR-");
                if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                    query = query.Where(col => (
                        col.JobTitle.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.GradeLevel.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.Status.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.NoOfPosition.ToString().ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                          col.RequisitionID.ToString().Equals(id)));

                return query;

 
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public override bool Add(Requisition entity)
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

        public override bool Update(Requisition entity)
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
    public interface IRequisitionRepository : IRepository<Requisition>
    {
        IEnumerable<RequisitionVM> GetAllRequisitionByParam(RequisitionVM param,DateTime fromDate,DateTime toDate,IEnumerable<UserDepartment> departments, bool isAdmin, string type);
    }
}
