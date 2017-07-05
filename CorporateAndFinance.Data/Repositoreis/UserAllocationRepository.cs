using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Core.Helper.Structure;
using System.Data.Entity;
using CorporateAndFinance.Core.Helper.Extension;

namespace CorporateAndFinance.Data.Repositoreis
{
    public class UserAllocationRepository : RepositoryBase<UserAllocation>, IUserAllocationRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(UserAllocationRepository));
        public UserAllocationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<UserAllocationVM> GetAllUserAllocationByParam(UserAllocationVM param, DateTime fromDate, DateTime toDate, IEnumerable<UserDepartment> userDepartments, bool isAdmin, string type)
        {
            try
            {
                List<long> deptId = new List<long>();
                foreach (var dept in userDepartments)
                    deptId.Add(dept.DepartmentID);

                IQueryable<UserAllocationVM> query = null;
                if (type == RequestStatus.My_Request)
                {
                    query = (from ua in DbContext.UserAllocations.AsNoTracking()
                             join dept in DbContext.Departments.AsNoTracking() on ua.RequestedDepartmentID equals dept.DepartmentID

                             join tempReq in DbContext.Requisitions.AsNoTracking()
                             on ua.RequisitionID equals tempReq.RequisitionID into Requisition
                             from tempReq in Requisition.DefaultIfEmpty()

                             join tempUser in DbContext.Users.AsNoTracking()
                             on ua.UserID equals tempUser.Id into User
                             from tempUser in User.DefaultIfEmpty()

                             where !ua.Status.Equals(RequestStatus.Deleted) && (deptId.Contains(ua.RequestedDepartmentID))
                             orderby ua.CreatedOn descending
                             where (DbFunctions.TruncateTime(ua.CreatedOn) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(ua.CreatedOn) <= DbFunctions.TruncateTime(toDate))

                             select new UserAllocationVM
                             {
                                 RequisitionID = tempReq.RequisitionID,
                                 UserAllocationID = ua.UserAllocationID,
                                 UserName = tempUser.FirstName +" "+tempUser.LastName,
                                 Percentage = ua.Percentage,
                                 DepartmentName = (from reqDept in DbContext.Departments where reqDept.DepartmentID == ua.DepartmentID select reqDept.Name).FirstOrDefault(),
                                 DepartmentID = dept.DepartmentID,
                                 RequestedDepartmentID = ua.RequestedDepartmentID,
                                 RequestedDepartmentName = dept.Name,
                                 Status = ua.Status,
                                 Comments = ua.Comments,
                                 CreatedOn = ua.CreatedOn,
                                 RequestType = ua.RequisitionID.HasValue ? "Requisition" : "Allocation"
                             });

                }
                else
                {
                    query = (from ua in DbContext.UserAllocations.AsNoTracking()
                             join dept in DbContext.Departments.AsNoTracking() on ua.DepartmentID equals dept.DepartmentID
                             
                             join tempReq in DbContext.Requisitions.AsNoTracking()
                             on ua.RequisitionID equals tempReq.RequisitionID into Requisition
                             from tempReq in Requisition.DefaultIfEmpty()

                             join tempUser in DbContext.Users.AsNoTracking()
                             on ua.UserID equals tempUser.Id into User
                             from tempUser in User.DefaultIfEmpty()

                          
                             orderby ua.CreatedOn descending
                             where (DbFunctions.TruncateTime(ua.CreatedOn) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(ua.CreatedOn) <= DbFunctions.TruncateTime(toDate))
                             && (ua.IsActive && type.Equals(RequestStatus.Approved) || (!ua.IsActive && type.Equals(RequestStatus.Pending)) || (!ua.IsActive && type.Equals(RequestStatus.Rejected))) && ua.Status.Equals(type)  
                             && !ua.Status.Equals(RequestStatus.Deleted)  && (deptId.Contains(ua.DepartmentID))
                             select new UserAllocationVM
                             {
                                 RequisitionID = tempReq.RequisitionID,
                                 UserAllocationID = ua.UserAllocationID,
                                 UserName = tempUser.FirstName + " " + tempUser.LastName,
                                 Percentage = ua.Percentage,
                                 RequestedDepartmentName = (from reqDept in DbContext.Departments where reqDept.DepartmentID == ua.RequestedDepartmentID select reqDept.Name).FirstOrDefault(),
                                 DepartmentID = ua.DepartmentID,
                                 RequestedDepartmentID = ua.RequestedDepartmentID,
                                 DepartmentName = dept.Name,
                                 Status = ua.Status,
                                 Comments = ua.Comments,
                                 CreatedOn = ua.CreatedOn,
                                 RequestType = ua.RequisitionID.HasValue ? "Requisition":"Allocation"
                             });

                }

                query = GetUserAllocationFiltersOrderQuery(query, param);




                int totalRecord = query.Count();

                var requisition = query.Skip(param.DTObject.start).Take(param.DTObject.length).ToList().Select(index => new UserAllocationVM
                {
                    RequisitionID = index.RequisitionID,
                    UserAllocationID = index.UserAllocationID,
                    UserName = index.UserName,
                    Percentage = index.Percentage,
                    DepartmentName = index.DepartmentName, 
                    DepartmentID = index.DepartmentID,
                    RequestedDepartmentID = index.RequestedDepartmentID,
                    RequestedDepartmentName = index.RequestedDepartmentName,
                    Status = index.Status,
                    Comments = index.Comments,
                    CreatedOn = index.CreatedOn,
                    RequestType = index.RequestType,
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

        private IQueryable<UserAllocationVM> GetUserAllocationFiltersOrderQuery(IQueryable<UserAllocationVM> query, UserAllocationVM param, bool forAll = false)
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
                        case "Comments":
                            if (!forAll)
                                query = query.Where(col => col.Comments.ToUpper().Contains(columnData.search.value.ToUpper()));
                            break;
                    }
                }

                string id = Utility.CovertID(param.DTObject.search.value, "UAL-");
                if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                    query = query.Where(col => (
                        col.RequestType.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.UserName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.DepartmentName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                          col.RequestedDepartmentName.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                         col.Percentage.ToString().ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                          col.Comments.ToString().ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                          col.UserAllocationID.ToString().Equals(id)));

                return query;


            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public override bool Add(UserAllocation entity)
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

        public override bool Update(UserAllocation entity)
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
    public interface IUserAllocationRepository : IRepository<UserAllocation>
    {
        IEnumerable<UserAllocationVM> GetAllUserAllocationByParam(UserAllocationVM param, DateTime frdate, DateTime tdate, IEnumerable<UserDepartment> userDepartments, bool isAdmin, string type);
    }
}
