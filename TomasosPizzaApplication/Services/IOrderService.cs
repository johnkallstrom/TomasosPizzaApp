using System.Threading.Tasks;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Services
{
    public interface IOrderService
    {
        Task<Bestallning> CreateOrder();
    }
}
