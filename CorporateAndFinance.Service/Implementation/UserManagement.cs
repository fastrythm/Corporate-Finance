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
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;


        public UserManagement(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return userRepository.GetAll();
        }

    }
}
