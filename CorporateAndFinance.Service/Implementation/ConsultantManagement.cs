using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Implementation
{
    public class ConsultantManagement: IConsultantManagement
    {
        private readonly IConsultantRepository consultantRepository;
        private readonly IUnitOfWork unitOfWork;


        public ConsultantManagement(IConsultantRepository consultantRepository, IUnitOfWork unitOfWork)
        {
            this.consultantRepository = consultantRepository;
            this.unitOfWork = unitOfWork;
        }
       
        public Consultant GetConsultantByNumber(int number)
        {
            return consultantRepository.Get(x => x.ConsultantNumber == number);
        }

        public void SaveConsultant()
        {
            unitOfWork.Commit();
        }

        public bool Add(Consultant model)
        {
            return consultantRepository.Add(model);
        }

        public IEnumerable<Consultant> GetAllConsultants()
        {
            return consultantRepository.GetMany(consult => consult.IsActive == true && consult.IsDeleted == false);
        }
    }
}
