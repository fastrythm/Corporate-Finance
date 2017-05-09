
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using log4net;
using System;

namespace CorporateAndFinance.Data.Repositories
{
    public class UserTaskDetailRepository : RepositoryBase<UserTaskDetail>, IUserTaskDetailRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(UserTaskDetailRepository));
        public UserTaskDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public override bool Add(UserTaskDetail entity)
        {
            try
            {
                entity.IsActive = true;
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

        public bool DeActivePreviousTaskDetails(long taskId)
        {
            try
            {

                var userTaskDetails = base.GetMany(x => x.UserTaskID == taskId);

                if (userTaskDetails != null)
                {
                    foreach (UserTaskDetail taskDetail in userTaskDetails)
                    {
                        taskDetail.LastModified = DateTime.Now;
                        taskDetail.IsActive = false;
                        base.Update(taskDetail);
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
                return false;
            }
        }
    }

    public interface IUserTaskDetailRepository : IRepository<UserTaskDetail>
    {
        bool DeActivePreviousTaskDetails(long taskId);
    }



}
