using System.Linq;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public class ProduktRepository : IProduktRepository
    {
        private readonly TomasosContext _context;

        public ProduktRepository(TomasosContext context)
        {
            _context = context;
        }

        public void Add(Produkt produkt)
        {
            _context.Add(produkt);
            _context.SaveChanges();
        }

        public Produkt Fetch(int id)
        {
            return _context.Produkt.FirstOrDefault(x => x.ProduktId == id);
        }

        public void Update(Produkt produkt)
        {
            var currentProdukt = _context.Produkt.FirstOrDefault(p => p.ProduktId == produkt.ProduktId);

            _context.Entry(currentProdukt).CurrentValues.SetValues(produkt);
            _context.SaveChanges();
        }
    }
}
