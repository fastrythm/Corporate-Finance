using CorporateAndFinance.Service.Interface;
using System.Collections.Generic;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositories;
using CorporateAndFinance.Core.ViewModel;
using System;

namespace CorporateAndFinance.Service.Implementation
{
    public class UserTaskManagement : IUserTaskManagement
    {
        private readonly IUserTaskRepository userTaskRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserTaskManagement(IUserTaskRepository userTaskRepository, IUnitOfWork unitOfWork)
        {
            this.userTaskRepository = userTaskRepository;
            this.unitOfWork = unitOfWork;
        }
        public bool Add(UserTask model)
        {
            return userTaskRepository.Add(model);
        }

        public bool Delete(UserTask model)
        {
            return userTaskRepository.Update(model);
        }

        public bool Update(UserTask model)
        {
            return userTaskRepository.Update(model);
        }

        public IEnumerable<UserTaskVM> GetTaskByCriteria(UserTaskVM param,string userId, IEnumerable<UserDepartment> departments)
        {
            return userTaskRepository.GetTaskByCriteria(param,userId, departments);
        }

        public void SaveUserTask()
        {
            unitOfWork.Commit();
        }

        public UserTask GetUserTaskById(long id)
        {
            return userTaskRepository.GetById(id);
        }

        public UserTaskEmailVM GetUserTaskEmailDetails(long taskId)
        {
            return userTaskRepository.GetUserTaskEmailDetails(taskId);
        }
    }
}
