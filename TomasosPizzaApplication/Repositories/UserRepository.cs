using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TomasosContext context;

        public UserRepository(TomasosContext context)
        {
            this.context = context;
        }

        public void AddCustomer(Kund kund)
        {
            context.Kund.Add(kund);
            context.SaveChanges();
        }
    }
}
