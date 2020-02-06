using Microsoft.EntityFrameworkCore;
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

        public void AddCustomer(Kund customer)
        {
            _context.Kund.Add(customer);
            _context.SaveChanges();
        }

        public Kund GetCustomerByID(string id)
        {
            return _context.Kund.FirstOrDefault(k => k.UserId == id);
        }

        public void UpdateCustomer(Kund updatedCustomer)
        {
            var currentCustomer = _context.Kund.FirstOrDefault(k => k.UserId == updatedCustomer.UserId);

            _context.Entry(currentCustomer).CurrentValues.SetValues(updatedCustomer);
            _context.SaveChanges();
        }
    }
}
