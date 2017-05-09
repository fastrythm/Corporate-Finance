using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using CorporateAndFinance.Core.Helper.Extension;
using CorporateAndFinance.Core.Helper.Structure;
using log4net;

namespace CorporateAndFinance.Data.Repositories
{
    public class UserTaskRepository : RepositoryBase<UserTask>, IUserTaskRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(UserTaskRepository));
        public UserTaskRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public override bool Add(UserTask entity)
        {
            try
            {
                entity.IsDeleted = false;
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

        public IEnumerable<UserTaskVM> GetTaskByCriteria(UserTaskVM param, string userId)
        {
            try
            {

                IQueryable<UserTaskVM> query = (from Record in DbContext.UserTasks.AsNoTracking()
                                                join UTD in DbContext.UserTaskDetails.AsNoTracking()
                                                on Record.UserTaskID equals UTD.UserTaskID
                                                join FU in DbContext.Users.AsNoTracking()
                                                on UTD.FromUserID.ToString() equals FU.Id
                                                join TU in DbContext.Users.AsNoTracking()
                                                on UTD.ToUserID.ToString() equals TU.Id

                                                where !Record.IsDeleted && UTD.IsActive
                                                orderby Record.CreatedOn descending
                                                where (userId == "1") || (UTD.ToUserID.ToString() == userId && UTD.Status != "Closed")
                                                select new UserTaskVM
                                                {
                                                    UserTaskID = Record.UserTaskID,
                                                    Title = Record.Title,
                                                    Description = Record.Description,
                                                    Type = Record.Type,
                                                    Priority = Record.Priority,
                                                    DueDate = Record.DueDate,
                                                    FromUser = FU.FirstName + " " + FU.LastName,
                                                    ToUser = TU.FirstName + " " + TU.LastName,
                                                    Remarks = UTD.Remarks,
                                                    Status = UTD.Status,
                                                    CreatedDate = Record.CreatedOn
                                                });


                query = GetUserTaskFiltersOrderQuery(query, param);


                int totalRecord = query.Count();

                var pettyCashList = query.Skip(param.DTObject.start).Take(param.DTObject.length).ToList().Select(index => new UserTaskVM
                {
                    UserTaskID = index.UserTaskID,
                    Title = index.Title,
                    Description = index.Description,
                    Type = index.Type,
                    Priority = index.Priority,
                    DueDate = index.DueDate,
                    FromUser = index.FromUser,
                    ToUser = index.ToUser,
                    Remarks = index.Remarks,
                    Status = index.Status,
                    CreatedDate= index.CreatedDate,
                    DTObject = new DataTablesViewModel() { TotalRecordsCount = totalRecord }
                }).ToList();



                return pettyCashList;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        private IQueryable<UserTaskVM> GetUserTaskFiltersOrderQuery(IQueryable<UserTaskVM> query, UserTaskVM param, bool forAll = false)
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
                        case "Description":
                            if (!forAll)
                                query = query.Where(col => col.Description.ToUpper().Contains(columnData.search.value.ToUpper()));
                            break;
                    }
                }

                string id = Utility.CovertID(param.DTObject.search.value, "UT-");

                if (param.DTObject.search.value != null && !string.IsNullOrEmpty(param.DTObject.search.value))
                    query = query.Where(col => (
                        col.Description.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.Title.ToUpper().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.Status.ToString().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.Priority.ToString().Contains(param.DTObject.search.value.ToUpper()) ||
                        col.UserTaskID.ToString().Equals(id)));
                return query;


                return query;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public override bool Update(UserTask entity)
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

        public UserTaskEmailVM GetUserTaskEmailDetails(long taskId)
        {
            try
            {

                UserTaskEmailVM result = (from Record in DbContext.UserTasks.AsNoTracking()
                                          where !Record.IsDeleted
                                          orderby Record.CreatedOn descending
                                          where (Record.UserTaskID == taskId)
                                          select new UserTaskEmailVM
                                          {
                                              Title = Record.Title,
                                              TicketNumber = Record.UserTaskID.ToString(),
                                              Description = Record.Description,
                                              Priority = Record.Priority,
                                              DueDate = Record.DueDate
                                          }).SingleOrDefault();

                if (!string.IsNullOrEmpty(result.TicketNumber))
                    result.TicketNumber = "UT-" + Convert.ToInt64(result.TicketNumber).ToString("000000");

                result.UserTaskDetails = GetUserTaskDetails(taskId);
                return result;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }

        public List<UserTaskDetailVM> GetUserTaskDetails(long taskId)
        {
            try
            {

                List<UserTaskDetailVM> result = (from UTD in DbContext.UserTaskDetails.AsNoTracking()
                                                 join FU in DbContext.Users.AsNoTracking()
                                                  on UTD.FromUserID.ToString() equals FU.Id
                                                 join TU in DbContext.Users.AsNoTracking()
                                                 on UTD.ToUserID.ToString() equals TU.Id
                                                 orderby UTD.CreatedOn descending
                                                 where (UTD.UserTaskID == taskId)
                                                 select new UserTaskDetailVM
                                                 {
                                                     FromUser = FU.FirstName + " " + FU.LastName,
                                                     FromUserEmail = FU.Email,
                                                     ToUser = TU.FirstName + " " + TU.LastName,
                                                     ToUserEmail = TU.Email,
                                                     Status = UTD.Status,
                                                     Remarks = UTD.Remarks,
                                                     IsActive = UTD.IsActive

                                                 }).ToList();


                return result;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return null;
            }
        }
    }

    public interface IUserTaskRepository : IRepository<UserTask>
    {
        IEnumerable<UserTaskVM> GetTaskByCriteria(UserTaskVM param, string userId);
        UserTaskEmailVM GetUserTaskEmailDetails(long taskId);
        List<UserTaskDetailVM> GetUserTaskDetails(long taskId);
    }



}
