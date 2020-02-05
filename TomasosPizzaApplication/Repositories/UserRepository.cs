using System.Linq;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TomasosContext _context;

        public UserRepository(TomasosContext context)
        {
            _context = context;
        }

        public void AddCustomer(Kund kund)
        {
            _context.Kund.Add(kund);
            _context.SaveChanges();
        }

        public Kund GetCustomerByID(string id)
        {
            return _context.Kund.FirstOrDefault();
        }
    }
}
