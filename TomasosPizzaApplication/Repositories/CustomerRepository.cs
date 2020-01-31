using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private TomasosContext context;

        public CustomerRepository(TomasosContext context)
        {
            this.context = context;
        }

        public bool IsValidUser(string username, string password)
        {
            bool isValidUser = context.Kund
                .Any(c => c.AnvandarNamn == username && c.Losenord == password);
            return isValidUser;
        }

        public bool UsernameAlreadyExists(Kund kund)
        {
            var usernameAlreadyExists = context.Kund
                .Any(k => k.AnvandarNamn == kund.AnvandarNamn);
            return usernameAlreadyExists;
        }

        public void RegisterNewCustomer(Kund kund)
        {
            context.Kund.Add(kund);
            context.SaveChanges();
        }
    }
}
