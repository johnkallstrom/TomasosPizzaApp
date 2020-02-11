using System.Collections.Generic;
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

        public void Add(Kund customer)
        {
            _context.Kund.Add(customer);
            _context.SaveChanges();
        }

        public List<Kund> FetchAll()
        {
            return _context.Kund.ToList();
        }

        public Kund Fetch(string id)
        {
            return _context.Kund.FirstOrDefault(k => k.UserId == id);
        }

        public void Update(Kund updatedCustomer)
        {
            var currentCustomer = _context.Kund.FirstOrDefault(k => k.UserId == updatedCustomer.UserId);

            _context.Entry(currentCustomer).CurrentValues.SetValues(updatedCustomer);
            _context.SaveChanges();
        }
    }
}
