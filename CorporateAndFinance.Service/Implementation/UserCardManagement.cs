using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Core.ViewModel;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositories;
using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Implementation
{
    public class UserCardManagement : IUserCardManagement  
    {
        private readonly IUserCardRepository userCardRepository;
        private readonly IUnitOfWork unitOfWork;


        public UserCardManagement(IUserCardRepository userCardRepository, IUnitOfWork unitOfWork)
        {
            this.userCardRepository = userCardRepository;
            this.unitOfWork = unitOfWork;
        }

        public void SaveUserCard()
        {
            unitOfWork.Commit();
        }


        public UserCard GetUserCard(long id)
        {
            return userCardRepository.GetById(id);
        }

        public UserCard GetUserCardByAccountNumber(string number)
        {
            return userCardRepository.Get(x => x.CardNumber.Contains(number));
        }

        public bool Delete(UserCard model)
        {
            return userCardRepository.Delete(model);
        }

        public bool Add(UserCard model)
        {
            return userCardRepository.Add(model);
        }

        public bool Update(UserCard model)
        {
            throw new NotImplementedException();
        }

    }
}
