using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;

namespace TomasosPizzaApplication.Services
{
    public interface IAdminService
    {
        Task<IdentityResult> AddToPremiumRole(ApplicationUser user);
        Task<IdentityResult> AddToRegularRole(ApplicationUser user);
    }
}
