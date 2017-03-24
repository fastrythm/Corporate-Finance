using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;


namespace CorporateAndFinance.Data.Repositories
{
    public class GadgetRepository : RepositoryBase<Gadget>, IGadgetRepository
    {
        public GadgetRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IGadgetRepository : IRepository<Gadget>
    {

    }
}
