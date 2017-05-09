using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositories;

namespace CorporateAndFinance.Service.Implementation
{
    public class UserTaskDetailManagement : IUserTaskDetailManagement
    {
        private readonly IUserTaskDetailRepository userTaskDetailRepository;
        private readonly IUnitOfWork unitOfWork;


        public UserTaskDetailManagement(IUserTaskDetailRepository userTaskDetailRepository, IUnitOfWork unitOfWork)
        {
            this.userTaskDetailRepository = userTaskDetailRepository;
            this.unitOfWork = unitOfWork;
        }
        public bool Add(UserTaskDetail model)
        {
            return userTaskDetailRepository.Add(model);
        }

        public IEnumerable<UserTaskDetail> GetAllTaskDetailsByTaskId(long taskId)
        {
            return userTaskDetailRepository.GetMany(x => x.UserTaskID == taskId);
        }

        public void SaveUserTaskDetail()
        {
            unitOfWork.Commit();
        }

        public bool Delete(long taskId)
        {
            return userTaskDetailRepository.DeActivePreviousTaskDetails(taskId);
        }

        public UserTaskDetail GetActiveTaskDetailsByTaskId(long taskId)
        {
            return userTaskDetailRepository.Get(x => x.UserTaskID == taskId && x.IsActive);
        }
    }
}
