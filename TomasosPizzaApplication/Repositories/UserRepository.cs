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

        public void AddUser(Kund customer)
        {
            _context.Kund.Add(customer);
            _context.SaveChanges();
        }

        public List<Kund> FetchAllUsers()
        {
            return _context.Kund.ToList();
        }

        public Kund FetchUserByID(string id)
        {
            return _context.Kund.FirstOrDefault(k => k.UserId == id);
        }

        public void UpdateUser(Kund updatedCustomer)
        {
            var currentCustomer = _context.Kund.FirstOrDefault(k => k.UserId == updatedCustomer.UserId);

            _context.Entry(currentCustomer).CurrentValues.SetValues(updatedCustomer);
            _context.SaveChanges();
        }
    }
}
