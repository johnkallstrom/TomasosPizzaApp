using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    // All entity framework and database communication happends here in the repository.

    public interface IKundRepository
    {
        void RegisterCustomer(Kund kund);
    }

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
    }
}
