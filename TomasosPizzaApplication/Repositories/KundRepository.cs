using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public class KundRepository : IKundRepository
    {
        private TomasosContext context;

        public KundRepository(TomasosContext context)
        {
            this.context = context;
        }

        public void RegisterCustomer(Kund kund)
        {
            context.Kund.Add(kund);
            context.SaveChanges();
        }

        public Kund SignIn(Kund kund)
        {
            var user = context.Kund
                .FirstOrDefault(c => c.AnvandarNamn == kund.AnvandarNamn && c.Losenord == kund.Losenord);

            return user;
        }

        public bool UsernameAlreadyExists(Kund kund)
        {
            var usernameAlreadyExists = context.Kund.Any(k => k.AnvandarNamn == kund.AnvandarNamn);
            return usernameAlreadyExists ? true : false;
        }
    }
}
