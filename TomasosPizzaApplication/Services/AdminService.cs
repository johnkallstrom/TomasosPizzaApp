using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;

namespace TomasosPizzaApplication.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToPremiumRole(ApplicationUser user)
        {
            await _userManager.RemoveFromRoleAsync(user, "RegularUser");
            var result = await _userManager.AddToRoleAsync(user, "PremiumUser");
            return result;
        }

        public async Task<IdentityResult> AddToRegularRole(ApplicationUser user)
        {
            await _userManager.RemoveFromRoleAsync(user, "PremiumUser");
            var result = await _userManager.AddToRoleAsync(user, "RegularUser");
            return result;
        }
    }
}
